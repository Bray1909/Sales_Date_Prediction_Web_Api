﻿
namespace Domain.Entities
{
    public class Employee
    {
        public int EmpId { get; set; }              
        public string LastName { get; set; }         
        public string FirstName { get; set; }       
        public string Title { get; set; }            
        public string TitleOfCourtesy { get; set; }  
        public DateTime BirthDate { get; set; }      
        public DateTime HireDate { get; set; }       
        public string Address { get; set; }          
        public string City { get; set; }            
        public string? Region { get; set; }          
        public string? PostalCode { get; set; }      
        public string Country { get; set; }          
        public string Phone { get; set; }            
        public int? MgrId { get; set; }              

        public Employee? Manager { get; set; }       
    }
}
