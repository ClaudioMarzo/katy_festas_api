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