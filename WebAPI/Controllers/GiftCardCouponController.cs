using BusinessLayer.Models.GiftCardCoupon.Requests;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class GiftCardCouponController : ControllerBase
{
    private readonly IGiftCardCouponService _couponService;
    private readonly ICartService _cartService;

    public GiftCardCouponController(IGiftCardCouponService couponService, ICartService cartService)
    {
        _couponService = couponService;
        _cartService = cartService;
    }

    [HttpGet]
    [Route("validate/{code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ValidateCoupon(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return BadRequest(new { message = "Coupon code is required." });
        }

        try
        {
            var coupon = await _couponService.ValidateAndGetCouponAsync(code);

            if (coupon == null)
            {
                return NotFound(new { message = "Coupon not found." });
            }

            return Ok(coupon);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [Route("mark-used")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MarkAsUsed([FromBody] CouponOrderRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (request.CouponId <= 0 || request.OrderId <= 0)
        {
            return BadRequest(new { message = "Invalid coupon ID or order ID." });
        }

        var order = await _cartService.GetByIdAsync(request.OrderId);
        if (order == null)
        {
            return NotFound(new { message = $"Order with ID {request.OrderId} not found." });
        }

        try
        {
            var success = await _couponService.MarkAsUsedAsync(request.CouponId, request.OrderId);

            if (!success)
            {
                return NotFound(new { message = $"Coupon with ID {request.CouponId} not found." });
            }

            return Ok(new { message = "Coupon marked as used successfully." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}