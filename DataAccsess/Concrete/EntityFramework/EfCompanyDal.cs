using Core.DataAccsess.EntityFramework;
using Core.Entities.Concrete;
using DataAccsess.Abstract;
using DataAccsess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Concrete.EntityFramework
{
    public class EfCompanyDal : EfEntityRepositoryBase<Company, ContextDb>, ICompanyDal
    {
        public UserCompany GetCompany(int userId)
        {
            using (var context = new ContextDb())//Yeni nesneyi context'e ekleyerek veritabanına kaydetmek için işaretlemek.
			{
              var result = context.UserCompanies.Where(p=>p.UserId == userId).FirstOrDefault();
                return result;
            }
        }

        public void UserCompanyAdd(int userId, int companyId)
        {
            using (var context = new ContextDb())
            {
                UserCompany userCompany = new UserCompany()
                {
                    UserId = userId,
                    CompanyId = companyId,
                    AddedAt = DateTime.Now,
                    IsActive = true,
                };
                context.UserCompanies.Add(userCompany); //yeni oluşturulan nesneyi veritabanına kaydeder.
				context.SaveChanges();
				/*
				 using ifadesi, ContextDb nesnesinin uygun şekilde atılmasını sağlar.
                 Kod, belirli ayrıntılarla bir UserCompany nesnesi oluşturur.
                 Nesneyi context'e eklemek, veritabanına kaydetmek için işaretler.
				*/
			}
		}
    }
}
