namespace KatyFestas.Application.Common;

/// <summary>
/// Request padrão para consultas paginadas
/// </summary>
public class PagedRequest
{
    private const int MaxPageSize = 30;
    private int _pageSize = 20;

    /// <summary>
    /// Número da página (inicia em 1)
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Quantidade de itens por página (máximo 30)
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}
