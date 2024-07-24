﻿using BookStoreApp.API.Data;

namespace BookStoreApp.API.Repositories
{
    public interface IAuthorsRepository : IGenericRepository<Author>
    {
        Task CreerDonneesVirtualisation();
    }
}
