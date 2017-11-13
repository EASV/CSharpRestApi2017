using CustomerAppDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using CustomerAppDAL.UOW;

namespace CustomerAppDAL
{
    public class DALFacade
    {
        DbOptions opt;
        public DALFacade(DbOptions opt){
            this.opt = opt;
        }

        public IUnitOfWork UnitOfWork
		{
			get
			{
                return new UnitOfWork(opt);
			}
		}

    }
}
