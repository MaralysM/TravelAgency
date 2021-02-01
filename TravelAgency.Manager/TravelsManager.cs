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
    public class TravelsManager : ITravelsManager
    {

        public ITravelsRepository TravelsRepository { get; }

        public TravelsManager(ITravelsRepository travelsRepository)
        {
            TravelsRepository = travelsRepository;
        }

        public async Task<IEnumerable<Travels>> All()
        {
            try
            {
                return await TravelsRepository.AllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to get to list");
            }
        }

        public async Task Save(Travels entity)
        {
            try
            {
                if (entity.ID_Travels == 0)
                {
                    await TravelsRepository.Add(entity);
                }
                else
                {
                    await TravelsRepository.UpdateAsync(entity);
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

        public async Task<Travels> FindById(int id)
        {
            try
            {
                return await TravelsRepository.FindByIdAsync(id);
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
                Travels entity = await TravelsRepository.FindByIdAsync(id);
                TravelsRepository.Remove(entity);
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
