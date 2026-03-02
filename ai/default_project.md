> Clean Architecture + SOLID + Cross-Cutting Concerns — v1.0
> 

---

## Visão Geral da Arquitetura

O projeto segue **Clean Architecture** dividida em 4 camadas com responsabilidades bem definidas. A regra fundamental é que a direção de dependência é sempre de fora para dentro.

```
API            → Application + Infrastructure
Application    → Domain
Infrastructure → Domain
Domain         → ninguém
```

---

## Princípios SOLID Aplicados

### S — Single Responsibility

Cada classe tem uma única razão para mudar.

- ItemService → cuida apenas das regras de negócio de itens
- CloudflareR2Service → cuida apenas do upload de arquivos
- ExceptionHandlingMiddleware → cuida apenas do tratamento de erros
- Validators separados dos DTOs — validação é uma responsabilidade distinta

### O — Open/Closed

Aberto para extensão, fechado para modificação.

- Para adicionar AWS S3, cria-se AwsS3Service implementando IStorageService — sem modificar nenhuma classe existente
- IBaseRepository<T> é estendido por cada repositório sem modificar a base

### L — Liskov Substitution

Qualquer implementação de uma interface pode substituir outra sem quebrar o sistema.

- CloudflareR2Service e AwsS3Service implementam IStorageService — Application não sabe qual está sendo usada

### I — Interface Segregation

Interfaces pequenas e específicas.

- IBaseRepository<T> define métodos comuns
- IItemRepository extende apenas com métodos específicos de item
- IRentalRepository extende apenas com métodos específicos de aluguel

### D — Dependency Inversion

Classes de alto nível não dependem de classes de baixo nível — ambas dependem de abstrações.

- Controllers dependem de IItemService, não de ItemService
- Services dependem de IItemRepository, não de ItemRepository

---

## Conceitos de Nível Pleno

### Unit of Work

Garante que operações multi-tabela acontecem em uma única transação — ou tudo persiste ou nada persiste.

Exemplo crítico: confirmar aluguel deve decrementar estoque ao mesmo tempo.

```csharp
public interface IUnitOfWork
{
    IItemRepository Items { get; }
    IRentalRepository Rentals { get; }
    IEstimateRepository Estimates { get; }
    IPartnerRepository Partners { get; }
    ICategoryRepository Categories { get; }
    ICustomerRepository Customers { get; }
    IUserRepository Users { get; }
    Task<int> CommitAsync();
}

// Uso no RentalService
await _unitOfWork.Rentals.ConfirmAsync(rentalId);
await _unitOfWork.Items.DecrementStockAsync(itemId, quantity);
await _unitOfWork.CommitAsync(); // persiste os dois ou nenhum
```

### Paginação

Todo endpoint de listagem tem paginação.

```csharp
public class PagedRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

public class PagedResponse<T>
{
    public IEnumerable<T> Data { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalItems { get; init; }
    public int TotalPages { get; init; }
}
```

### FluentValidation

Validação de DTOs separada da lógica de negócio.

```csharp
public class CreateItemValidator : AbstractValidator<CreateItemDto>
{
    public CreateItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Preço deve ser maior que zero");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Estoque não pode ser negativo");
    }
}
```

### JWT — Autenticação

Access token com expiração de 7 dias para admin e 1 dia para cliente. Sem Refresh Token — decisão consciente para o porte e contexto do projeto.

```
POST /api/v1/auth/login → retorna access_token + expires_at
Authorization: Bearer {token} → header em todas as rotas protegidas
```

### CorrelationId — Rastreabilidade

UUID único gerado por middleware que percorre todas as camadas e aparece em todos os logs e respostas.

```
Request chega → CorrelationId: "abc-123" gerado
       ↓
Percorre Controller → Service → Repository
       ↓
Aparece na resposta e em todos os logs
       ↓
Erro em produção → filtra logs por "abc-123" → rastreia tudo
```

### Middleware Global de Exceções

Intercepta todas as exceções e devolve resposta padronizada. Controllers ficam sem try/catch.

```
DomainException       → 400 Bad Request
NotFoundException     → 404 Not Found
UnauthorizedException → 401 Unauthorized
Exception genérica    → 500 Internal Server Error
```

### Health Check

```
GET /health → { "status": "healthy", "database": "ok", "storage": "ok" }
```

### Rate Limiting

Limita requisições por minuto. Configurado nativamente no .NET 7+, sem biblioteca extra.

---

## Respostas Padronizadas

### Interfaces

```csharp
public interface IApiResponse
{
    bool Success { get; }
    string Message { get; }
    string CorrelationId { get; }
}

public interface ISuccessResponse<T> : IApiResponse
{
    T Data { get; }
}

public interface IErrorResponse : IApiResponse
{
    int StatusCode { get; }
    IEnumerable<string> Errors { get; }
}
```

### GET

```json
{
  "success": true,
  "message": "Item encontrado",
  "correlationId": "abc-123",
  "data": { "id": "uuid", "name": "Toalha Redonda Branca", "price": 50.00 }
}
```

### GET — Listagem Paginada

```json
{
  "success": true,
  "message": "Itens encontrados",
  "correlationId": "abc-123",
  "data": [...],
  "pagination": { "page": 1, "pageSize": 20, "totalItems": 150, "totalPages": 8 }
}
```

### POST

```json
{
  "success": true,
  "message": "Item criado com sucesso",
  "correlationId": "abc-123",
  "location": "/api/v1/items/uuid-gerado",
  "data": { "id": "uuid-gerado", "name": "Toalha Redonda Branca" }
}
```

### Erro 400

```json
{
  "success": false,
  "message": "Dados inválidos",
  "correlationId": "abc-123",
  "statusCode": 400,
  "errors": ["Nome é obrigatório", "Preço deve ser maior que zero"]
}
```

### Erro 500

```json
{
  "success": false,
  "message": "Erro interno do servidor",
  "correlationId": "abc-123",
  "statusCode": 500,
  "errors": []
}
```

---

## Estrutura de Pastas

```
KatyFestas.sln
├── src/
│   ├── KatyFestas.Domain/
│   │   ├── Entities/
│   │   │   ├── Store.cs
│   │   │   ├── Category.cs
│   │   │   ├── Item.cs
│   │   │   ├── ItemPhoto.cs
│   │   │   ├── Partner.cs
│   │   │   ├── PartnerPhoto.cs
│   │   │   ├── User.cs
│   │   │   ├── Customer.cs
│   │   │   ├── Estimate.cs
│   │   │   ├── EstimateItem.cs
│   │   │   ├── Rental.cs
│   │   │   └── RentalItem.cs
│   │   ├── Enums/
│   │   │   └── RentalStatus.cs
│   │   ├── Exceptions/
│   │   │   └── DomainException.cs
│   │   └── Interfaces/
│   │       ├── IUnitOfWork.cs
│   │       ├── Repositories/
│   │       │   ├── IBaseRepository.cs
│   │       │   ├── IItemRepository.cs
│   │       │   ├── IRentalRepository.cs
│   │       │   ├── IEstimateRepository.cs
│   │       │   ├── IPartnerRepository.cs
│   │       │   ├── ICategoryRepository.cs
│   │       │   ├── ICustomerRepository.cs
│   │       │   └── IUserRepository.cs
│   │       └── Services/
│   │           └── IStorageService.cs
│   │
│   ├── KatyFestas.Application/
│   │   ├── Common/
│   │   │   ├── PagedRequest.cs
│   │   │   └── PagedResponse.cs
│   │   ├── DTOs/
│   │   │   ├── Item/
│   │   │   │   ├── ItemResponseDto.cs
│   │   │   │   ├── CreateItemDto.cs
│   │   │   │   └── UpdateItemDto.cs
│   │   │   ├── Rental/
│   │   │   │   ├── RentalResponseDto.cs
│   │   │   │   ├── CreateRentalDto.cs
│   │   │   │   └── UpdateRentalStatusDto.cs
│   │   │   ├── Estimate/
│   │   │   │   ├── EstimateResponseDto.cs
│   │   │   │   └── CreateEstimateDto.cs
│   │   │   ├── Partner/
│   │   │   │   ├── PartnerResponseDto.cs
│   │   │   │   ├── CreatePartnerDto.cs
│   │   │   │   └── UpdatePartnerDto.cs
│   │   │   ├── Category/
│   │   │   │   ├── CategoryResponseDto.cs
│   │   │   │   ├── CreateCategoryDto.cs
│   │   │   │   └── UpdateCategoryDto.cs
│   │   │   ├── Auth/
│   │   │   │   ├── LoginDto.cs
│   │   │   │   ├── RegisterDto.cs
│   │   │   │   └── TokenResponseDto.cs
│   │   │   └── Customer/
│   │   │       └── CustomerResponseDto.cs
│   │   ├── Interfaces/
│   │   │   ├── Services/
│   │   │   │   ├── IItemService.cs
│   │   │   │   ├── IRentalService.cs
│   │   │   │   ├── IEstimateService.cs
│   │   │   │   ├── IPartnerService.cs
│   │   │   │   ├── ICategoryService.cs
│   │   │   │   ├── ICustomerService.cs
│   │   │   │   └── IAuthService.cs
│   │   │   └── Responses/
│   │   │       ├── IApiResponse.cs
│   │   │       ├── ISuccessResponse.cs
│   │   │       └── IErrorResponse.cs
│   │   ├── Services/
│   │   │   ├── ItemService.cs
│   │   │   ├── RentalService.cs
│   │   │   ├── EstimateService.cs
│   │   │   ├── PartnerService.cs
│   │   │   ├── CategoryService.cs
│   │   │   ├── CustomerService.cs
│   │   │   └── AuthService.cs
│   │   └── Validators/
│   │       ├── Item/
│   │       │   ├── CreateItemValidator.cs
│   │       │   └── UpdateItemValidator.cs
│   │       ├── Rental/
│   │       │   ├── CreateRentalValidator.cs
│   │       │   └── UpdateRentalStatusValidator.cs
│   │       ├── Estimate/
│   │       │   └── CreateEstimateValidator.cs
│   │       └── Auth/
│   │           ├── LoginValidator.cs
│   │           └── RegisterValidator.cs
│   │
│   ├── KatyFestas.Infrastructure/
│   │   ├── Persistence/
│   │   │   ├── AppDbContext.cs
│   │   │   ├── UnitOfWork.cs
│   │   │   ├── Migrations/
│   │   │   └── Configurations/
│   │   │       ├── ItemConfiguration.cs
│   │   │       ├── RentalConfiguration.cs
│   │   │       ├── EstimateConfiguration.cs
│   │   │       └── PartnerConfiguration.cs
│   │   ├── Repositories/
│   │   │   ├── BaseRepository.cs
│   │   │   ├── ItemRepository.cs
│   │   │   ├── RentalRepository.cs
│   │   │   ├── EstimateRepository.cs
│   │   │   ├── PartnerRepository.cs
│   │   │   ├── CategoryRepository.cs
│   │   │   ├── CustomerRepository.cs
│   │   │   └── UserRepository.cs
│   │   └── Storage/
│   │       └── CloudflareR2Service.cs
│   │
│   └── KatyFestas.API/
│       ├── Controllers/
│       │   ├── Public/
│       │   │   ├── ItemsController.cs
│       │   │   ├── CategoriesController.cs
│       │   │   ├── PartnersController.cs
│       │   │   ├── EstimatesController.cs
│       │   │   └── RentalsController.cs
│       │   ├── Admin/
│       │   │   ├── AdminItemsController.cs
│       │   │   ├── AdminCategoriesController.cs
│       │   │   ├── AdminPartnersController.cs
│       │   │   ├── AdminRentalsController.cs
│       │   │   ├── AdminEstimatesController.cs
│       │   │   └── AdminUsersController.cs
│       │   └── HealthController.cs
│       ├── Middlewares/
│       │   ├── ExceptionHandlingMiddleware.cs
│       │   └── CorrelationIdMiddleware.cs
│       ├── Responses/
│       │   ├── ApiResponse.cs
│       │   ├── CreatedResponse.cs
│       │   └── ErrorResponse.cs
│       ├── Program.cs
│       └── appsettings.json
│
└── tests/
    ├── KatyFestas.Domain.Tests/
    │   └── Entities/
    ├── KatyFestas.Application.Tests/
    │   └── Services/
    └── KatyFestas.Infrastructure.Tests/
        └── Repositories/
```

---

## Tabela de Decisões

| Decisão | Princípio | Justificativa |
| --- | --- | --- |
| Interface para cada serviço e repositório | D — Dependency Inversion | Dependência de abstrações, não de implementações concretas |
| IBaseRepository genérico | I — Interface Segregation | Evita repetição, cada repositório extende só o que precisa |
| Single Responsibility por classe | S — Single Responsibility | Cada classe tem uma única razão para mudar |
| Validators separados dos DTOs | S — Single Responsibility | Validação é responsabilidade distinta da estrutura de dados |
| Open/Closed via interfaces | O — Open/Closed | Novo storage = nova classe, sem modificar código existente |
| Unit of Work | — | Transações multi-tabela — ou tudo persiste ou nada persiste |
| Paginação em todas as listagens | — | Evita degradação de performance com volume crescente |
| FluentValidation | — | Padrão do mercado .NET, validação testável e separada |
| JWT sem Refresh Token | — | Decisão consciente — porte e contexto não justificam complexidade extra |
| CorrelationId | — | Rastreabilidade completa em produção |
| Middleware global de exceções | — | Controllers limpos, tratamento centralizado |
| Rate Limiting nativo .NET | — | Proteção contra abuso sem biblioteca extra |
| Health Check | — | Provedores usam para verificar disponibilidade do serviço |

---

*Próxima etapa: Setup do projeto — criação da solution, Docker e primeiras configurações.*