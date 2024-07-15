using System.ComponentModel.DataAnnotations;

namespace MarketOnl.Models
{
	public class OrderViewModel
	{
		[Required(ErrorMessage = "Tên khách hàng không để trống")]
		public string CustomerName { get; set; }
		[Required(ErrorMessage = "Số điện thoại không để trống")]
		public string Phone { get; set; }
		[Required(ErrorMessage = "Địa chỉ khổng để trống")]
		public string Address { get; set; }
		public string Email { get; set; }
		public string? CustomerId { get; set; }
		public int TypePayment { get; set; }
		
	}
}
