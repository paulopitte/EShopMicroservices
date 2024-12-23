﻿namespace Catalog.API.Products.CreateProduct;



public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);




internal sealed class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{




    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            Name = command.Name,
            Category = command.Category,    
            Description = command.Description,  
            ImageFile = command.ImageFile,
            Price = command.Price   
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);


        return await Task.FromResult(new CreateProductResult(product.Id));
    }
}

