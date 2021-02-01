using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Services;
using TravelAgency.Data;
using TravelAgency.Data.Repositories.Abstractions;
using TravelAgency.Entities;
using TravelAgency.Entities.Enums;
using TravelAgency.Manager.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TravelAgency.Manager
{
    public class TravellersManager : ITravellersManager
    {

        public ITravellersRepository TravellersRepository { get; }

        public TravellersManager(ITravellersRepository travellersRepository)
        {
            TravellersRepository = travellersRepository;
        }

        public async Task<IEnumerable<Travellers>> All()
        {
            try
            {
                return await TravellersRepository.AllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to get to list");
            }
        }

        public async Task Save(Travellers entity)
        {
            try
            {
                if (entity.ID_Travellers == 0)
                {
                    await TravellersRepository.Add(entity);
                }
                else
                {
                    await TravellersRepository.UpdateAsync(entity);
                }
            }
            catch (UniqueKeyException ex)
            {
                throw new Exception("Cannot insert or update a value duplicate" + ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot add a register" + ex);
            }
        }

        public async Task<Travellers> FindById(int id)
        {
            try
            {
                return await TravellersRepository.FindByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to search");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Travellers entity = await TravellersRepository.FindByIdAsync(id);
                TravellersRepository.Remove(entity);
            }
            catch (DeleteWithRelationshipException ex)
            {
                throw new Exception("The record you are trying to delete is related to another");
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to delete");
            }
        }      
    }

}
