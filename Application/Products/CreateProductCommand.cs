using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products
{
    public record CreateProductCommand(string name, decimal price) : IRequest<int>;
    internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Product>();
            await repo.AddAsync(new Product(request.name, request.price));
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
