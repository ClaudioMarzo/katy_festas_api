# 🎯 Plano de Ação — Rodar a API Katy Festas

## ✅ ETAPA 0 — Validação (CONCLUÍDA)

**Status:** ✅ As entidades do Domain estão seguindo o padrão Clean Architecture perfeitamente!

- ✅ Encapsulamento correto
- ✅ Métodos de fábrica estáticos
- ✅ Validação de domínio
- ✅ Sem vazamento de lógica de negócio

---

## 📋 ETAPA 1 — Configurar EF Core (Configurations)

**Objetivo:** Mapear as entidades para o banco de dados PostgreSQL

**O que criar:**
```
src/KatyFestas.Infrastructure/Persistence/Configurations/
├── StoreConfiguration.cs
├── CategoryConfiguration.cs
├── ItemConfiguration.cs
├── ItemPhotoConfiguration.cs
├── PartnerConfiguration.cs
├── PartnerPhotoConfiguration.cs
├── UserConfiguration.cs
├── CustomerConfiguration.cs
├── EstimateConfiguration.cs
├── EstimateItemConfiguration.cs
├── RentalConfiguration.cs
└── RentalItemConfiguration.cs
```

**Exemplo de uma configuração:**
```csharp
public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);
        
        builder.Property(x => x.Price)
            .HasColumnType("decimal(18,2)");
        
        builder.HasOne(x => x.Store)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.Photos)
            .WithOne(x => x.Item)
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasQueryFilter(x => !x.IsDeleted); // Soft delete global
    }
}
```

**Por quê?** O EF Core precisa saber como mapear as propriedades privadas e relacionamentos.

---

## 📋 ETAPA 2 — Criar Repositories Concretos

**Objetivo:** Implementar acesso a dados na camada Infrastructure

**O que criar:**
```
src/KatyFestas.Infrastructure/Repositories/
├── BaseRepository.cs
├── ItemRepository.cs
├── CategoryRepository.cs
├── RentalRepository.cs
├── EstimateRepository.cs
├── PartnerRepository.cs
├── CustomerRepository.cs
└── UserRepository.cs
```

**Exemplo de BaseRepository:**
```csharp
public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
        => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        entity.SetDeletedAt(); // Soft delete
        Update(entity);
    }
}
```

**Exemplo de ItemRepository:**
```csharp
public class ItemRepository : BaseRepository<Item>, IItemRepository
{
    public ItemRepository(AppDbContext context) : base(context) { }

    public async Task<PagedResponse<Item>> GetPagedAsync(int page, int pageSize)
    {
        var totalItems = await _dbSet.CountAsync();
        var items = await _dbSet
            .Include(x => x.Category)
            .Include(x => x.Photos)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<Item>
        {
            Data = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };
    }

    public async Task<bool> ExistsByNameAsync(string name, Guid storeId)
        => await _dbSet.AnyAsync(x => x.Name == name && x.StoreId == storeId);
}
```

---

## 📋 ETAPA 3 — Criar UnitOfWork

**Objetivo:** Gerenciar transações entre múltiplos repositórios

**O que criar:**
```
src/KatyFestas.Infrastructure/Persistence/UnitOfWork.cs
```

**Implementação:**
```csharp
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IItemRepository Items { get; }
    public ICategoryRepository Categories { get; }
    public IRentalRepository Rentals { get; }
    public IEstimateRepository Estimates { get; }
    public IPartnerRepository Partners { get; }
    public ICustomerRepository Customers { get; }
    public IUserRepository Users { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Items = new ItemRepository(context);
        Categories = new CategoryRepository(context);
        Rentals = new RentalRepository(context);
        Estimates = new EstimateRepository(context);
        Partners = new PartnerRepository(context);
        Customers = new CustomerRepository(context);
        Users = new UserRepository(context);
    }

    public async Task<int> CommitAsync()
        => await _context.SaveChangesAsync();

    public void Dispose()
        => _context.Dispose();
}
```

---

## 📋 ETAPA 4 — Criar DTOs e Validators

**Objetivo:** Definir contratos de entrada e saída + validação

**O que criar:**
```
src/KatyFestas.Application/DTOs/
├── Item/ (já existe, revisar)
├── Rental/
│   ├── RentalResponseDto.cs
│   ├── CreateRentalDto.cs
│   └── UpdateRentalStatusDto.cs
├── Estimate/
│   ├── EstimateResponseDto.cs
│   └── CreateEstimateDto.cs
├── Partner/
│   ├── PartnerResponseDto.cs
│   ├── CreatePartnerDto.cs
│   └── UpdatePartnerDto.cs
├── Category/
│   ├── CategoryResponseDto.cs
│   ├── CreateCategoryDto.cs
│   └── UpdateCategoryDto.cs
├── Customer/
│   ├── CustomerResponseDto.cs
│   ├── CreateCustomerDto.cs
│   └── UpdateCustomerDto.cs
└── Auth/
    ├── LoginDto.cs
    ├── RegisterDto.cs
    └── TokenResponseDto.cs

src/KatyFestas.Application/Validators/
├── Item/ (já existe)
├── Rental/
├── Estimate/
├── Partner/
├── Category/
├── Customer/
└── Auth/

src/KatyFestas.Application/Common/
├── PagedRequest.cs
└── PagedResponse.cs
```

**Exemplo de CreateRentalDto:**
```csharp
public record CreateRentalDto
{
    public Guid CustomerId { get; init; }
    public DateTime EventDate { get; init; }
    public string? Notes { get; init; }
    public List<RentalItemDto> Items { get; init; } = [];
}

public record RentalItemDto
{
    public Guid ItemId { get; init; }
    public int Quantity { get; init; }
}
```

**Validator correspondente:**
```csharp
public class CreateRentalValidator : AbstractValidator<CreateRentalDto>
{
    public CreateRentalValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Cliente é obrigatório");

        RuleFor(x => x.EventDate)
            .GreaterThan(DateTime.Now).WithMessage("Data do evento deve ser no futuro");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Aluguel deve ter pelo menos um item");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.ItemId).NotEmpty();
            item.RuleFor(x => x.Quantity).GreaterThan(0);
        });
    }
}
```

---

## 📋 ETAPA 5 — Criar Services (Application)

**Objetivo:** Implementar a lógica de aplicação (orquestração)

**O que criar:**
```
src/KatyFestas.Application/Services/
├── ItemService.cs
├── CategoryService.cs
├── RentalService.cs
├── EstimateService.cs
├── PartnerService.cs
├── CustomerService.cs
└── AuthService.cs

src/KatyFestas.Application/Interfaces/Services/
├── IItemService.cs
├── ICategoryService.cs
├── IRentalService.cs
├── IEstimateService.cs
├── IPartnerService.cs
├── ICustomerService.cs
└── IAuthService.cs
```

**Exemplo de ItemService:**
```csharp
public class ItemService : IItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStorageService _storageService;

    public ItemService(IUnitOfWork unitOfWork, IStorageService storageService)
    {
        _unitOfWork = unitOfWork;
        _storageService = storageService;
    }

    public async Task<ItemResponseDto> CreateAsync(CreateItemDto dto)
    {
        // Validar se categoria existe
        var category = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId)
            ?? throw new NotFoundException("Categoria não encontrada");

        // Criar item
        var item = Item.Create(
            dto.StoreId, 
            dto.CategoryId, 
            dto.Name, 
            dto.Description, 
            dto.Price, 
            dto.StockQuantity
        );

        await _unitOfWork.Items.AddAsync(item);
        await _unitOfWork.CommitAsync();

        return MapToResponseDto(item);
    }

    public async Task<PagedResponse<ItemResponseDto>> GetPagedAsync(PagedRequest request)
    {
        var pagedItems = await _unitOfWork.Items.GetPagedAsync(request.Page, request.PageSize);
        
        return new PagedResponse<ItemResponseDto>
        {
            Data = pagedItems.Data.Select(MapToResponseDto),
            Page = pagedItems.Page,
            PageSize = pagedItems.PageSize,
            TotalItems = pagedItems.TotalItems,
            TotalPages = pagedItems.TotalPages
        };
    }

    private ItemResponseDto MapToResponseDto(Item item) => new()
    {
        Id = item.Id,
        Name = item.Name,
        Description = item.Description,
        Price = item.Price,
        StockQuantity = item.StockQuantity,
        IsActive = item.IsActive,
        CategoryName = item.Category.Name
    };
}
```

---

## 📋 ETAPA 6 — Criar Middlewares

**Objetivo:** Interceptação global de requisições

**O que criar:**
```
src/KatyFestas.API/Middlewares/
├── ExceptionHandlingMiddleware.cs (já existe, revisar)
└── CorrelationIdMiddleware.cs
```

**CorrelationIdMiddleware:**
```csharp
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private const string CorrelationIdHeader = "X-Correlation-ID";

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers[CorrelationIdHeader].FirstOrDefault() 
            ?? Guid.NewGuid().ToString();

        context.Items["CorrelationId"] = correlationId;
        context.Response.Headers[CorrelationIdHeader] = correlationId;

        await _next(context);
    }
}
```

---

## 📋 ETAPA 7 — Criar Controllers

**Objetivo:** Expor endpoints REST

**O que criar:**
```
src/KatyFestas.API/Controllers/
├── Public/
│   ├── ItemsController.cs
│   ├── CategoriesController.cs
│   ├── PartnersController.cs
│   ├── EstimatesController.cs
│   └── RentalsController.cs
├── Admin/
│   ├── AdminItemsController.cs
│   ├── AdminCategoriesController.cs
│   ├── AdminPartnersController.cs
│   ├── AdminRentalsController.cs
│   ├── AdminEstimatesController.cs
│   └── AdminUsersController.cs
├── AuthController.cs
└── HealthController.cs
```

**Exemplo de ItemsController (público):**
```csharp
[ApiController]
[Route("api/v1/items")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PagedRequest request)
    {
        var result = await _itemService.GetPagedAsync(request);
        var correlationId = HttpContext.Items["CorrelationId"]?.ToString();

        return Ok(new
        {
            success = true,
            message = "Itens encontrados",
            correlationId,
            data = result.Data,
            pagination = new
            {
                result.Page,
                result.PageSize,
                result.TotalItems,
                result.TotalPages
            }
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _itemService.GetByIdAsync(id);
        var correlationId = HttpContext.Items["CorrelationId"]?.ToString();

        return Ok(new
        {
            success = true,
            message = "Item encontrado",
            correlationId,
            data = item
        });
    }
}
```

**Exemplo de AdminItemsController:**
```csharp
[ApiController]
[Route("api/v1/admin/items")]
[Authorize(Roles = "Admin")]
public class AdminItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public AdminItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateItemDto dto)
    {
        var item = await _itemService.CreateAsync(dto);
        var correlationId = HttpContext.Items["CorrelationId"]?.ToString();

        return CreatedAtAction(nameof(ItemsController.GetById), "Items", 
            new { id = item.Id }, 
            new
            {
                success = true,
                message = "Item criado com sucesso",
                correlationId,
                data = item
            });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateItemDto dto)
    {
        await _itemService.UpdateAsync(id, dto);
        var correlationId = HttpContext.Items["CorrelationId"]?.ToString();

        return Ok(new
        {
            success = true,
            message = "Item atualizado com sucesso",
            correlationId
        });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _itemService.DeleteAsync(id);
        var correlationId = HttpContext.Items["CorrelationId"]?.ToString();

        return Ok(new
        {
            success = true,
            message = "Item deletado com sucesso",
            correlationId
        });
    }
}
```

---

## 📋 ETAPA 8 — Configurar Injeção de Dependência (Program.cs)

**Objetivo:** Registrar todos os serviços no container DI

**Adicionar ao Program.cs:**
```csharp
// ── Repositories e UnitOfWork ─────────────────────────────────
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ── Services ───────────────────────────────────────────────────
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IEstimateService, EstimateService>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// ── Storage (Cloudflare R2) ────────────────────────────────────
builder.Services.AddScoped<IStorageService, CloudflareR2Service>();

// ── Health Checks ──────────────────────────────────────────────
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

// ── Middlewares ────────────────────────────────────────────────
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// ── Health Endpoint ────────────────────────────────────────────
app.MapHealthChecks("/health");
```

---

## 📋 ETAPA 9 — Gerar e Aplicar Migrations

**Objetivo:** Criar o banco de dados

**Comandos:**
```bash
# Entrar na pasta da API
cd src/KatyFestas.API

# Gerar migration inicial
dotnet ef migrations add InitialCreate --project ../KatyFestas.Infrastructure

# Aplicar migration
dotnet ef database update --project ../KatyFestas.Infrastructure
```

**Pré-requisitos:**
- PostgreSQL rodando (via Docker ou local)
- Connection string configurada no appsettings.json

---

## 📋 ETAPA 10 — Rodar a API

**Pré-requisitos:**
1. PostgreSQL rodando
2. appsettings.json configurado

**Comandos:**
```bash
# Via .NET CLI
cd src/KatyFestas.API
dotnet run

# Via Docker Compose (recomendado)
docker-compose up --build
```

**Testar:**
```bash
# Health check
curl http://localhost:5000/health

# Listar itens (público)
curl http://localhost:5000/api/v1/items

# Login
curl -X POST http://localhost:5000/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@katyfestas.com","password":"senha123"}'

# Criar item (admin - precisa do token)
curl -X POST http://localhost:5000/api/v1/admin/items \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer SEU_TOKEN_AQUI" \
  -d '{"name":"Toalha Branca","description":"...","price":50.00,"stockQuantity":10,"categoryId":"uuid"}'
```

---

## 🎯 Resumo das Etapas

| # | Etapa | Descrição | Depende de |
|---|-------|-----------|------------|
| 0 | ✅ Validação | Verificar entidades | — |
| 1 | EF Core Configurations | Mapear entidades para DB | 0 |
| 2 | Repositories | Implementar acesso a dados | 1 |
| 3 | UnitOfWork | Gerenciar transações | 2 |
| 4 | DTOs e Validators | Contratos de entrada/saída | — |
| 5 | Services | Lógica de aplicação | 3, 4 |
| 6 | Middlewares | Interceptação global | — |
| 7 | Controllers | Endpoints REST | 5, 6 |
| 8 | DI (Program.cs) | Registrar serviços | 2, 3, 5 |
| 9 | Migrations | Criar banco de dados | 1, 8 |
| 10 | Rodar API | Subir aplicação | 9 |

---

## 🚀 Próximo Passo Recomendado

**Começar pela ETAPA 1 — EF Core Configurations**

Esta é a base para tudo funcionar. Sem as configurações, o EF Core não consegue mapear as propriedades privadas das entidades.

Quer que eu comece implementando a Etapa 1? 🎯
