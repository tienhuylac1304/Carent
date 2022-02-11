//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeThongThueXe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.HopDongThueXes = new HashSet<HopDongThueXe>();
            this.SoDatXes = new HashSet<SoDatXe>();
        }
        public int IDUser { get; set; }
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "It's not empty!!")]
        public string HoTen { get; set; }
        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "It's not empty!!")]
        [DataType("date")]
        public string NgaySinh { get; set; }
        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "It's not empty!!")]
        public string SDT { get; set; }
        [Display(Name = "User name")]
        [Required(ErrorMessage = "It's not empty!!")]
        public string Account { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "It's not empty!!")]
        [DataType("password")]
        public string PasswordUser { get; set; }
        [NotMapped]
        [Display(Name = "Confirm pass")]
        [Required(ErrorMessage = "It's not empty!!")]
        [Compare("PasswordUser")]
        public string ConfirmPass { get; set; }
        public int IDLoaiUser { get; set; }
        public int SoXeDangThue { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "It's not empty!!")]
        public string DiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HopDongThueXe> HopDongThueXes { get; set; }
        public virtual LoaiUser LoaiUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SoDatXe> SoDatXes { get; set; }
    }
}