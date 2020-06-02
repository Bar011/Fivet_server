using System;
using System.Threading;
using System.Threading.Tasks;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fivet.Server
{
    internal class FivetService : IHostedService
    {
        private readonly ILogger<FivetService> _logger;

        private  readonly int _port = 8080; 

        private readonly Communicator _communicator; 

        private readonly TheSystemDisp_ _theSystem;

        // The Contracts
        private readonly ContratosDisp_ _contratos;


        public FivetService(ILogger<FivetService> logger, TheSystemDisp_ theSystem, ContratosDisp_ contratos)
        {
            _logger = logger;
            _logger.LogDebug("Building FivetService ..");
            _theSystem = theSystem;
            _contratos = contratos;
            _communicator = buildCommunicator();
        }

        public Task StartAsync(CancellationToken cancellationToken) {
        
        _logger.LogDebug("StartTing the FivetService..");

        var adapter = _communicator.createObjectAdapterWithEndpoints("TheSystem","top -z -t 15000 -p" + _port);

        //the interface 
        TheSystem theSystem = new TheSystemImpl();
        
        //Register in the comunicator
        adapter.add(theSystem, Util.stringToIdentity("TheSystem"));

        //Activation
        adapter.activate();

        //ALL ok
        return Task.CompletedTask;
    }
        public Task StopAsync(CancellationToken cancellationToken)
        {
             _logger.LogDebug("Stopping the FivetService..");
            _communicator.shutdown();
            _logger.LogDebug("Comunnicator stopped!!");
             return Task.CompletedTask;
        }
        private Communicator buildCommunicator()
        {
            Properties prop = Util.createProperties();
            prop.setProperty("Ice.Trace.Network", "3");
            
            InitializationData initializationData = new InitializationData();

            initializationData.properties =prop;

            return Ice.Util.initialize(initializationData);
        }
        
    }

    public class TheSystemImpl : TheSystemDisp_
    {
        public override long getDelay(long clientTime,Current current = null)
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - clientTime;
        }
    }
}
