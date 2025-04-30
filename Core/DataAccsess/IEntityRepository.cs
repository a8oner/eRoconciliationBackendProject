using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccsess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()//bu generic yapıdır
	//T bir class ve IEntity ile implement edilmesi gerekli(dikkat et Entities'de oluşturduğumuz tüm tablolar IEntity ile implement edilmişti) ve new'lenebilir olmalı.
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetList(Expression<Func<T, bool>> filter=null);//parantezin ilk kısmı sorgu yapabilirim ,null ise yapmayabilirim de anlamına gelir
		T Get(Expression<Func<T, bool>> filter = null);
    }
}
