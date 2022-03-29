using Fatca.Core.Configurations;
using Fatca.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Fatca.Core
{
    public class FatcaClient
    {
        private readonly FatcaSettings _setting;
        public IDictionary<string,string> Errors { get; private set; }
        public bool HasError { get; private set; }
        private readonly FatcaProcessor _processor;
        public ProcessingService processingService = null;
        public FatcaClient(FatcaSettings settings,FatcaProcessor processor)
        {
            if(processingService is null)
            processingService = new ProcessingService();
            processor.fileProcessingService = processingService;
            _setting = settings;
            _processor = processor;
            
        }
        private FatcaClient()
        {
            _setting = new FatcaSettings();
            _processor = FatcaProcessor.Create();
        }
        public static FatcaClient Create() => new FatcaClient();
        public void Execute(string path = "", bool loadFromFolder = false)
        {
            Validate();

            _processor.fileProcessingService = processingService;
            if (_setting.ValidateSchema)
                _processor
                    .Validate().Sign(loadFromFolder: loadFromFolder, path)
                    .Excypt().Apply();
            else
                _processor
                    .Sign(loadFromFolder: loadFromFolder, path)
                    .Excypt().Apply();

            Errors = _processor.ValidationErrors;
            HasError = _processor.HasError;
        }
        public void Clear()
        {
            _processor.ValidationErrors.Clear();
        }
        private void Validate()
        {
            Check(_setting.Certificate, nameof(_setting.Certificate));
            Check(_setting.Password, nameof(_setting.Password));
            Check(_setting.ReceiverPublicKey, nameof(_setting.ReceiverPublicKey));
            if (_setting.ValidateSchema)
                Check(_setting.SchemaFolder, nameof(_setting.SchemaFolder));

        }

        private void Check(string value, string parameterName = "value")
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException($"{parameterName} cannot be empty value");
        }
        
        
    }
}
