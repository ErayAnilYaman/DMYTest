﻿
namespace DMYTest.Data.Models
{
    
    #region Usings
using DMYTest.Data.Models.Abstract;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public class User : IEntity
    {
        
        private ICollection<Supplier> _suppliers;
        private ICollection<Cart> _carts;
        private ICollection<Employee> _employees;
        private ICollection<Sales> _sales;
        private ICollection<Comment> _comments;
        private ICollection<Order> _orders;
        private ICollection<CommentLikeModel> _likes;
        public User()
        {
            _sales = new HashSet<Sales>();
            _comments = new HashSet<Comment>();
            _carts = new HashSet<Cart>();
            _suppliers = new HashSet<Supplier>();
            _employees = new HashSet<Employee>();
            _orders = new HashSet<Order>();
            _likes = new HashSet<CommentLikeModel>();
        }
            
        [Key]
        public int ID { get; set; }
        

        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Adinizi Giriniz")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Soyadinizi Giriniz")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string LastName { get; set; }


        [Required(ErrorMessage ="Lutfen Doldurunuz")]
        [Display(Name = "Kullanici Adinizi Giriniz")]
        [StringLength(50, ErrorMessage = "Karakter sinirini gectiniz")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Sirket Adi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        [EmailAddress(ErrorMessage ="Lutfen gecerli bir mail giriniz")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Password Hash")]
        public byte[] PasswordHash { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Password Salt")]
        public byte[] PasswordSalt { get; set; }



        [Display(Name = "Durum")]
        public bool Status { get; set; }


        
        [Display(Name = "Kullanici Rolu")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string Role { get; set; }

        public virtual Customer Customer { get; set; }

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
        public virtual ICollection<Order> Orders{ get { return _orders;  } set { _orders = value; } }
        public virtual ICollection<CommentLikeModel> Likes { get { return _likes; } set { value = _likes; } }

    }
}