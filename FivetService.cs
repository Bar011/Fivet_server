using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Ice;
using model;

namespace Fivet.Server
{
    internal class FivetService : IHostedService
    {
        private readonly ILogger<FivetService> _logger;

        private  readonly int _port = 8080; 

        private readonly Communicator _communicator; 

        public FivetService(ILogger<FivetService> logger)
        {
                _logger =logger;
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
