namespace DMYTest.Data.Models
{
    using DMYTest.Data.Models.Abstract;
    #region Usings
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public class Customer : IEntity
    {
        private ICollection<Supplier> _suppliers;
        private ICollection<Cart> _carts;
        private ICollection<Employee> _employees;
        private ICollection<Sales> _sales;
        private ICollection<Comment> _comments;
        private ICollection<Order> _orders;
        public Customer()
        {
            _sales = new HashSet<Sales>();
            _comments = new HashSet<Comment>();
            _carts = new HashSet<Cart>();
            _suppliers = new HashSet<Supplier>();
            _employees = new HashSet<Employee>();
            _orders = new HashSet<Order>();
        }

        [Display(Name ="Musteri")]
        public int CustomerID { get; set; }

        
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Sirket Adi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string CompanyName { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Adres Adi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Lutfen Email Adres kismini doldurunuz!")]
        [StringLength(75, ErrorMessage = "Karakter sinirini gectiniz maksimum 75 karakter girebilirsiniz!")]
        [Display(Name = "Ofis Mail Adresi")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Telefon Numarasi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        [Phone]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Lutfen Ulkeyi Doldurunuz")]
        [Display(Name = "Ulke")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string Country { get; set; }


        [Required(ErrorMessage = "Lutfen Posta Kodunu Doldurunuz")]
        [Display(Name = "Posta Kodu")]
        [StringLength(5, ErrorMessage = "karakter sinirini gectiniz")]

        public string PostCode { get; set; }


        public virtual User User { get; set; }
        public virtual ICollection<Supplier> Suppliers
        {
            get
            {
                return this._suppliers;
            }
            set
            {
                this._suppliers = value;
            }
        }
        public virtual ICollection<Comment> Comments { get { return _comments; } set { _comments = value; } }
        public virtual ICollection<Employee> Employees
        {
            get
            {
                return this._employees;
            }
            set
            {
                this._employees = value;
            }
        }
        public virtual ICollection<Sales> Sales
        {
            get
            {
                return this._sales;
            }
            set
            {
                this._sales = value;
            }
        }
        public virtual ICollection<Cart> Carts
        {
            get
            {
                return this._carts;
            }
            set
            {
                this._carts = value;
            }
        }
        public virtual ICollection<Order> Orders { get { return _orders; } set { _orders = value; } }

    }
}