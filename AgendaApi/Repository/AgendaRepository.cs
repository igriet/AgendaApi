using AgendaApi.Interface.Data;
using AgendaApi.Logging;
using AgendaApi.Model;
using System.Diagnostics;
using System.Text.Json;

namespace AgendaApi.Repository
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly string _path = $"{Environment.CurrentDirectory}\\Data\\AgendaData.json";
        private readonly ILogManager _logger;

        public AgendaRepository(ILogManager logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Contact>> ReadAsync()
        {
            try
            {
                using FileStream jsonStream = new FileStream(_path, FileMode.Open);
                IEnumerable<Contact> agenda = await JsonSerializer.DeserializeAsync<IEnumerable<Contact>>(jsonStream);

                return agenda;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex?.Message);
                throw ex;
            }
        }

        public async Task<bool> WriteAsync(IEnumerable<Contact> newCollection)
        {
            try
            {
                string newJsonAgenda = JsonSerializer.Serialize<IEnumerable<Contact>>(newCollection);
                await File.WriteAllTextAsync(_path, newJsonAgenda);

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex?.Message);
                throw ex;
            }
        }
    }
}
