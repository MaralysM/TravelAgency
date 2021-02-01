
using TravelAgency.Data;
using TravelAgency.Entities;
using TravelAgency.Manager.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TravelAgency.Manager
{
    public class RequestsManager : IRequestsManager
    {
        public IRequestsRepository RequestsRepository { get; }

        public RequestsManager( IRequestsRepository requestsRepository)
        {
            RequestsRepository = requestsRepository;
        }

        public async Task<IEnumerable<Requests>> All()
        {
            try
            {
                return await RequestsRepository.AllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to get to list");
            }
        }

        public async Task Save(Requests entity)
        {
            try
            {
                if (entity.ID_Requests == 0)
                {
                    await RequestsRepository.Add(entity);
                }
                else
                {
                    await RequestsRepository.UpdateAsync(entity);
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

        public async Task<Requests> FindById(int id)
        {
            try
            {
                return await RequestsRepository.FindByIdAsync(id);
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
                Requests entity = await RequestsRepository.FindByIdAsync(id);
                RequestsRepository.Remove(entity);
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
