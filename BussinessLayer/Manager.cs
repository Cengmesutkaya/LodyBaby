using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class Manager
    {
        public CategoryManager repo_category;   
        public EmailManager repo_email;
        public TeamManager repo_team;
        public ProductManager repo_product;
        public MemberManager repo_member;
        public OrderManager repo_order;
        public CityManager repo_city;
        public TownManager repo_town;
        public OfferManager repo_offer;
        public OrderDetailManager repo_orderdetail;


        public Manager()
        {           
            repo_category = new CategoryManager();         
            repo_email = new EmailManager();
            repo_team = new TeamManager();
            repo_product = new ProductManager();
            repo_member = new MemberManager();
            repo_order = new OrderManager();
            repo_city = new CityManager();
            repo_town = new TownManager();
            repo_offer = new OfferManager();
            repo_orderdetail = new OrderDetailManager();
        }
    }
}
