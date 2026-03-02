# 🎯 Aggregates - Domain-Driven Design

Esta pasta organiza as entidades seguindo o padrão **Aggregate** do DDD.

## 📚 O que é um Aggregate?

Um **Aggregate** é um grupo de objetos de domínio que podem ser tratados como uma única unidade. O **Aggregate Root** é a entidade principal que controla o acesso aos demais objetos do grupo.

### Regras:

1. ✅ **Acesse filhos SEMPRE pelo Aggregate Root**
2. ❌ **Nunca modifique filhos diretamente**
3. ✅ **Transações devem respeitar limites do Aggregate**

---

## 🗂️ Estrutura

```
Aggregates/
├── ItemAggregate/
│   ├── Item.cs           (ROOT - controla ciclo de vida)
│   └── ItemPhoto.cs      (FILHO - gerenciado por Item)
│
├── RentalAggregate/
│   ├── Rental.cs         (ROOT)
│   └── RentalItem.cs     (FILHO)
│
├── EstimateAggregate/
│   ├── Estimate.cs       (ROOT)
│   └── EstimateItem.cs   (FILHO)
│
├── PartnerAggregate/
│   ├── Partner.cs        (ROOT)
│   └── PartnerPhoto.cs   (FILHO)
│
├── Category.cs           (ROOT sem filhos)
├── Customer.cs           (ROOT sem filhos)
├── Store.cs              (ROOT sem filhos)
└── User.cs               (ROOT sem filhos)
```

---

## 🎯 Exemplos de Uso

### ✅ CORRETO - Acessar filho pelo pai

```csharp
// 1. Buscar o Aggregate Root
var item = await _itemRepository.GetByIdAsync(itemId);

// 2. Adicionar filho através do método do Root
item.AddPhoto(url: "https://...", order: 1);

// 3. Salvar (UnitOfWork persiste tudo)
await _unitOfWork.CommitAsync();
```

### ❌ INCORRETO - Acessar filho diretamente

```csharp
// NÃO FAÇA ISSO!
var photo = new ItemPhoto();
await _photoRepository.AddAsync(photo); // ❌ Não existe PhotoRepository!
```

---

## 📋 DbContext - Aggregate Roots

No `AppDbContext`, **apenas Aggregate Roots** têm `DbSet<T>`:

```csharp
// ✅ TEM DbSet (Aggregate Roots)
public DbSet<Item> Items => Set<Item>();
public DbSet<Category> Categories => Set<Category>();
public DbSet<Store> Stores => Set<Store>();

// ❌ NÃO TEM DbSet (filhos)
// ItemPhoto é acessado via Item.Photos
// RentalItem é acessado via Rental.Items
```

---

## 🔗 Como Acessar Filhos?

Use `Include()` nas queries:

```csharp
var item = await _context.Items
    .Include(x => x.Photos)        // Carrega filhos
    .FirstOrDefaultAsync(x => x.Id == id);

// Agora você tem acesso às fotos
foreach (var photo in item.Photos)
{
    Console.WriteLine(photo.Url);
}
```

---

## 🚀 Benefícios

1. ✅ **Consistência**: Regras de negócio centralizadas no Root
2. ✅ **Encapsulamento**: Filhos não são modificados externamente
3. ✅ **Transações**: Limites claros de persistência
4. ✅ **Manutenibilidade**: Código organizado e previsível
5. ✅ **Performance**: Menos queries desnecessárias

---

## 📖 Referências

- [Microsoft - Aggregate Pattern](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-domain-model)
- [Martin Fowler - DDD Aggregate](https://martinfowler.com/bliki/DDD_Aggregate.html)
