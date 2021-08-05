using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountReository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountReository discountRepository, 
            ILogger<DiscountService> logger,
            IMapper mapper
            )
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,
                    $"Discount with ProductName={request.ProductName} Not Available"
                    ));
            }
            _logger.LogInformation($"Discount is retrieved for ProductName : {request.ProductName}" +
                $", Amount {coupon.Amount}");
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.CreateDiscount(coupon);

            _logger.LogInformation($"Discount is successfully created. Product Name {coupon.ProductName}");

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.UpdateDiscount(coupon);
            
            _logger.LogInformation($"Discount is successfully updated. Product Name {coupon.ProductName}");

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;

        }
        
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
            _logger.LogInformation($"Discount is successfully deleted.");
            var response = new DeleteDiscountResponse
            {
                Success = deleted
            };
            return response;
        }
    }
}
