using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 2 thư viện thiết kế class metada
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebSiteBanSach.Models.Metadata
{
    [MetadataTypeAttribute(typeof(SachMetadata))]
    //nối với class sach trong model
    public partial class Sach
    {
        internal sealed class SachMetadata
        {
            [Key]
            public int MaSach { get; set; }

            [Display(Name = "Tên sách")]
            //[Required(ErrorMessage = "{0} không được để trống")]
            [StringLength(50, ErrorMessage = "{0} không quá 50 kí tự")]
            public string TenSach { get; set; }

            [Display(Name = "Giá bán")]
            //[Required(ErrorMessage = "{0} không được để trống")]
            [Range(1000, 10000000, ErrorMessage = "{0} phải thuộc [1.000, 10.000.000]")]
            [DisplayFormat(DataFormatString = "{0:#,##0 vnđ}")]
            public Nullable <decimal> GiaBan { get; set; }

            [Display(Name = "Mô tả")]
            //[Required(ErrorMessage = "{0} không được để trống")]
            public string MoTa { get; set; }

            [Display(Name = "Ảnh bìa")]
            [StringLength(50, ErrorMessage = "{0} không quá 50 kí tự")]
            public string AnhBia { get; set; }

            [Display(Name = "Ngày tạo")]
            //[Required(ErrorMessage = "{0} không được để trống")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            //[DataType(DataType.Date)]
            public Nullable <System.DateTime> NgayCapNhat { get; set; }

            [Display(Name = "Số lượng")]
            //[Required(ErrorMessage = "{0} không được để trống")]
            [Range(0, 1000000, ErrorMessage = "{0} phải thuộc [0, 1.000.000]")]
            [DisplayFormat(DataFormatString = "{0:#,##0}")]
            public Nullable <int> SoLuongTon { get; set; }

            [Display(Name = "Nhà xuất bản")]
            //[Required(ErrorMessage = "{0} không được để trống")]
            public Nullable <int> MaNXB { get; set; }

            [Display(Name = "Chủ đề")]
            //[Required(ErrorMessage = "{0} không được để trống")]
            public Nullable <int> MaChuDe { get; set; }

            [Display(Name = "Tình trạng")]
            //[Required(ErrorMessage = "{0} không được để trống")]
            [Range(0, 1, ErrorMessage = "{0} phải thuộc [0, 1] 0: Hết hàng, 1: Còn hàng")]
            public Nullable <int> Moi { get; set; }

        }
    }
}