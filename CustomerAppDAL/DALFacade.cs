using CustomerAppDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using CustomerAppDAL.UOW;

namespace CustomerAppDAL
{
    public class DALFacade
    {
        public IUnitOfWork UnitOfWork
		{
			get
			{
				return new UnitOfWork();
			}
		}

    }
}
