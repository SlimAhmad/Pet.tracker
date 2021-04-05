using Pet.Tracker.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet.Tracker.Relational
{
    /// <summary>
    /// Stores and retrieves information about the client application 
    /// such as login credentials, messages, settings and so on
    /// in an SQLite database
    /// </summary>
    public class BaseClientDataStore : IClientDataStore
    {
        #region Protected Members

        /// <summary>
        /// The database context for the client data store
        /// </summary>
        protected ClientDataStoreDbContext mDbContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbContext">The database to use</param>
        public BaseClientDataStore(ClientDataStoreDbContext dbContext)
        {
            // Set local member
            mDbContext = dbContext;
        }
        #endregion


        #region Interface Implementation

        /// <summary>
        /// Determines if the current user has logged in credentials
        /// </summary>
        public async Task<bool> HasCredentialsAsync()
        {
            return await GetLoginCredentialsAsync() != null;
        }

        /// <summary>
        /// Makes sure the client data store is correctly set up
        /// </summary>
        /// <returns>Returns a task that will finish once setup is complete</returns>
        public async Task EnsureDataStoreAsync()
        {
            // Make sure the database exists and is created
            await mDbContext.Database.EnsureCreatedAsync();
        }

        #region User



        /// <summary>
        /// Gets the stored login credentials for this client
        /// </summary>
        /// <returns>Returns the login credentials if they exist, or null if none exist</returns>
        public Task<LoginCredentialsDataModel> GetLoginCredentialsAsync()
        {
            // Get the first column in the login credentials table, or null if none exist
            return Task.FromResult(mDbContext.LoginCredentials.FirstOrDefault());
        }

        /// <summary>
        /// Stores the given login credentials to the backing data store
        /// </summary>
        /// <param name="loginCredentials">The login credentials to save</param>
        /// <returns>Returns a task that will finish once the save is complete</returns>
        public async Task SaveLoginCredentialsAsync(LoginCredentialsDataModel loginCredentials)
        {
            // Clear all entries
            mDbContext.LoginCredentials.RemoveRange(mDbContext.LoginCredentials);


            // Add new one
            mDbContext.LoginCredentials.Add(loginCredentials);

            // Save changes
            await mDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes all login credentials stored in the data store
        /// </summary>
        /// <returns></returns>
        public async Task ClearAllLoginCredentialsAsync()
        {
            // Clear all entries
            mDbContext.LoginCredentials.RemoveRange(mDbContext.LoginCredentials);

            // Save changes
            await mDbContext.SaveChangesAsync();
        }
        #endregion

        #region Pet
        /// <summary>
        /// Gets the stored login credentials for this client
        /// </summary>
        /// <returns>Returns the login credentials if they exist, or null if none exist</returns>
        public Task<PetsCredentialsDataModel> GetPetCredentialsAsync()
        {
            // Get the first column in the login credentials table, or null if none exist
            return Task.FromResult(mDbContext.PetCredentials.FirstOrDefault());
        }

        /// <summary>
        /// Stores the given login credentials to the backing data store
        /// </summary>
        /// <param name="loginCredentials">The login credentials to save</param>
        /// <returns>Returns a task that will finish once the save is complete</returns>
        public async Task SavePetCredentialsAsync(PetsCredentialsDataModel petsCredentials)
        {
            // Clear all entries
            mDbContext.PetCredentials.RemoveRange(mDbContext.PetCredentials);

            // Add new one
            mDbContext.PetCredentials.Add(petsCredentials);

            // Save changes
            await mDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes all login credentials stored in the data store
        /// </summary>
        /// <returns></returns>
        public async Task ClearAllPetCredentialsAsync()
        {
            // Clear all entries
            mDbContext.PetCredentials.RemoveRange(mDbContext.PetCredentials);

            // Save changes
            await mDbContext.SaveChangesAsync();
        }
        #endregion

        #region Vet Centers
        /// <summary>
        /// Gets the stored login credentials for this client
        /// </summary>
        /// <returns>Returns the login credentials if they exist, or null if none exist</returns>
        public Task<VetCentersResultsApiModel> GetVetCentersAsync()
        {
            // Get the list of centers
            var result = mDbContext.VetCenter.Take(100).ToList();

            // Create a new list of results
            var results = new VetCentersResultsApiModel();

            // Add each centers details
            results.AddRange(result.Select(u => new VetCenterResultApiModel
            {

                Area = u.Area,
                Address = u.Address,
                State = u.State,
                Title = u.Title,
                Id = u.Id

            }));

            // Get the first column in the login credentials table, or null if none exist
            return Task.FromResult(results);
        }

        /// <summary>
        /// Stores the given login credentials to the backing data store
        /// </summary>
        /// <param name="loginCredentials">The login credentials to save</param>
        /// <returns>Returns a task that will finish once the save is complete</returns>
        public async Task SaveVetCentersAsync(VetCentersResultsApiModel vetCenterResults)
        {
            // Clear all entries
            mDbContext.VetCenter.RemoveRange(mDbContext.VetCenter);

            // Create a new list of results
            var results = new VetCentersResultsApiModel();

            // Add each centers details
            results.AddRange(vetCenterResults.Select(u => new VetCenterResultApiModel
            {

                Area = u.Area,
                Address = u.Address,
                State = u.State,
                Title = u.Title,
                Id = u.Id

            }));

            // Add new one
            mDbContext.VetCenter.AddRange(results);

            // Save changes
            await mDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes all login credentials stored in the data store
        /// </summary>
        /// <returns></returns>
        public async Task ClearAllVetCentersAsync()
        {
            // Clear all entries
            mDbContext.VetCenter.RemoveRange(mDbContext.VetCenter);

            // Save changes
            await mDbContext.SaveChangesAsync();
        }
        #endregion

        #region All Pets

        /// <summary>
        /// Gets the stored login credentials for this client
        /// </summary>
        /// <returns>Returns the login credentials if they exist, or null if none exist</returns>
        public Task<TransferPetsResultsApiModel> GetPetsAsync()
        {
            // Get the list of centers
            var result = mDbContext.Pets.Take(100).ToList();

            // Create a new list of results
            var results = new TransferPetsResultsApiModel();

            // Add each centers details
            results.AddRange(result.Select(u => new TransferPetResultApiModel
            {

                Age =u.Age,
                Breed = u.Breed,
                Email = u.Email,
                Height = u.Height,
                Weight = u.Weight,
                Image = u.Image,
                Gender = u.Gender,
                Description = u.Description,
                Status = u.Status,
                Name = u.Name,
                Neutered = u.Neutered,
                PetOwnerName = u.PetOwnerName,
                Phone = u.Phone,
                PetId = u.PetId,
                TransferPhone = u.TransferPhone,
                TransferEmail = u.TransferEmail,
                Transfer = u.Transfer,
                Username = u.Username

            }));

            // Get the first column in the login credentials table, or null if none exist
            return Task.FromResult(results);
        }

        /// <summary>
        /// Stores the given login credentials to the backing data store
        /// </summary>
        /// <param name="loginCredentials">The login credentials to save</param>
        /// <returns>Returns a task that will finish once the save is complete</returns>
        public async Task SavePetsAsync(TransferPetsResultsApiModel petsResults)
        {
            // Clear all entries
            mDbContext.Pets.RemoveRange(mDbContext.Pets);

            // Create a new list of results
            var results = new TransferPetsResultsApiModel();

            // Add each centers details
            results.AddRange(results.Select(u => new TransferPetResultApiModel
            {

                Age = u.Age,
                Breed = u.Breed,
                Email = u.Email,
                Height = u.Height,
                Weight = u.Weight,
                Image = u.Image,
                Gender = u.Gender,
                Description = u.Description,
                Status = u.Status,
                Name = u.Name,
                Neutered = u.Neutered,
                PetOwnerName = u.PetOwnerName,
                Phone = u.Phone,
                PetId = u.PetId,
                TransferPhone = u.TransferPhone,
                TransferEmail = u.TransferEmail,
                Transfer = u.Transfer,
                Username = u.Username,
                

            }));
            // Add new one
            mDbContext.Pets.AddRange(results);

            // Save changes
            await mDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes all login credentials stored in the data store
        /// </summary>
        /// <returns></returns>
        public async Task ClearAllPetsAsync()
        {
            // Clear all entries
            mDbContext.Pets.RemoveRange(mDbContext.Pets);

            // Save changes
            await mDbContext.SaveChangesAsync();
        }
        #endregion

        #endregion
    }
}
