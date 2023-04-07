using MediatR;

namespace Signaturit.App.Application;


public class GetProductQuery : IRequest<GetProductQueryResponse>
{
    public int ProductId { get; set; }
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
{
    public GetProductQueryHandler()
    {
 
    }

    public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {

        return new GetProductQueryResponse
        {
            Description = "product.Description",
            ProductId = 11,
            Price = 23.59
        };
    }
}

public class GetProductQueryResponse
{
    public int ProductId { get; set; }
    public string Description { get; set; } = default!;
    public double Price { get; set; }
}