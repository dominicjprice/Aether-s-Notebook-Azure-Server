<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="Aether_s_Notebook_Azure_Server" generation="1" functional="0" release="0" Id="77782ae2-b067-4677-b2fc-a4cea1b41456" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="Aether_s_Notebook_Azure_ServerGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="Web:http" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/LB:Web:http" />
          </inToChannel>
        </inPort>
        <inPort name="Web:https" protocol="https">
          <inToChannel>
            <lBChannelMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/LB:Web:https" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Certificate|Web:SSLCertificate" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapCertificate|Web:SSLCertificate" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Aethers.Notebook.DataAccess.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Aethers.Notebook.DataAccess.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Aethers.Notebook.Diagnostics.BufferQuota" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Aethers.Notebook.Diagnostics.BufferQuota" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Aethers.Notebook.Diagnostics.LogLevel" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Aethers.Notebook.Diagnostics.LogLevel" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Aethers.Notebook.Diagnostics.TransferPeriod" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Aethers.Notebook.Diagnostics.TransferPeriod" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Aethers.Notebook.Storage.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Aethers.Notebook.Storage.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Aethers.Notebook.Storage.Container.UnknownData" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Aethers.Notebook.Storage.Container.UnknownData" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Aethers.Notebook.Storage.Container.UploadedData" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Aethers.Notebook.Storage.Container.UploadedData" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Aethers.Notebook.Storage.Containers" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Aethers.Notebook.Storage.Containers" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Aethers.Notebook.Storage.Queue.UploadedData" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Aethers.Notebook.Storage.Queue.UploadedData" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Aethers.Notebook.Storage.Queues" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Aethers.Notebook.Storage.Queues" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Aethers.Notebook.Storage.Tables" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Aethers.Notebook.Storage.Tables" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="UploadWorkerInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorkerInstances" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.DataAccess.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.DataAccess.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Dataware.Description" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Dataware.Description" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Dataware.LogoUri" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Dataware.LogoUri" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Dataware.Namespace" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Dataware.Namespace" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Dataware.ResourceName" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Dataware.ResourceName" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Dataware.ResourceUri" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Dataware.ResourceUri" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Dataware.WebUri" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Dataware.WebUri" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Diagnostics.BufferQuota" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Diagnostics.BufferQuota" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Diagnostics.LogLevel" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Diagnostics.LogLevel" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Diagnostics.TransferPeriod" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Diagnostics.TransferPeriod" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Storage.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Storage.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Storage.Container.UnknownData" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Storage.Container.UnknownData" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Storage.Container.UploadedData" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Storage.Container.UploadedData" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Storage.Containers" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Storage.Containers" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Storage.Queue.UploadedData" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Storage.Queue.UploadedData" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Storage.Queues" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Storage.Queues" />
          </maps>
        </aCS>
        <aCS name="Web:Aethers.Notebook.Storage.Tables" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Aethers.Notebook.Storage.Tables" />
          </maps>
        </aCS>
        <aCS name="Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WebInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWebInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:Web:http">
          <toPorts>
            <inPortMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/http" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:Web:https">
          <toPorts>
            <inPortMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/https" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapCertificate|Web:SSLCertificate" kind="Identity">
          <certificate>
            <certificateMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/SSLCertificate" />
          </certificate>
        </map>
        <map name="MapUploadWorker:Aethers.Notebook.DataAccess.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Aethers.Notebook.DataAccess.ConnectionString" />
          </setting>
        </map>
        <map name="MapUploadWorker:Aethers.Notebook.Diagnostics.BufferQuota" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Aethers.Notebook.Diagnostics.BufferQuota" />
          </setting>
        </map>
        <map name="MapUploadWorker:Aethers.Notebook.Diagnostics.LogLevel" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Aethers.Notebook.Diagnostics.LogLevel" />
          </setting>
        </map>
        <map name="MapUploadWorker:Aethers.Notebook.Diagnostics.TransferPeriod" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Aethers.Notebook.Diagnostics.TransferPeriod" />
          </setting>
        </map>
        <map name="MapUploadWorker:Aethers.Notebook.Storage.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Aethers.Notebook.Storage.ConnectionString" />
          </setting>
        </map>
        <map name="MapUploadWorker:Aethers.Notebook.Storage.Container.UnknownData" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Aethers.Notebook.Storage.Container.UnknownData" />
          </setting>
        </map>
        <map name="MapUploadWorker:Aethers.Notebook.Storage.Container.UploadedData" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Aethers.Notebook.Storage.Container.UploadedData" />
          </setting>
        </map>
        <map name="MapUploadWorker:Aethers.Notebook.Storage.Containers" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Aethers.Notebook.Storage.Containers" />
          </setting>
        </map>
        <map name="MapUploadWorker:Aethers.Notebook.Storage.Queue.UploadedData" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Aethers.Notebook.Storage.Queue.UploadedData" />
          </setting>
        </map>
        <map name="MapUploadWorker:Aethers.Notebook.Storage.Queues" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Aethers.Notebook.Storage.Queues" />
          </setting>
        </map>
        <map name="MapUploadWorker:Aethers.Notebook.Storage.Tables" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Aethers.Notebook.Storage.Tables" />
          </setting>
        </map>
        <map name="MapUploadWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapUploadWorkerInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorkerInstances" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.DataAccess.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.DataAccess.ConnectionString" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Dataware.Description" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Dataware.Description" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Dataware.LogoUri" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Dataware.LogoUri" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Dataware.Namespace" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Dataware.Namespace" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Dataware.ResourceName" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Dataware.ResourceName" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Dataware.ResourceUri" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Dataware.ResourceUri" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Dataware.WebUri" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Dataware.WebUri" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Diagnostics.BufferQuota" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Diagnostics.BufferQuota" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Diagnostics.LogLevel" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Diagnostics.LogLevel" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Diagnostics.TransferPeriod" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Diagnostics.TransferPeriod" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Storage.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Storage.ConnectionString" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Storage.Container.UnknownData" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Storage.Container.UnknownData" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Storage.Container.UploadedData" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Storage.Container.UploadedData" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Storage.Containers" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Storage.Containers" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Storage.Queue.UploadedData" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Storage.Queue.UploadedData" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Storage.Queues" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Storage.Queues" />
          </setting>
        </map>
        <map name="MapWeb:Aethers.Notebook.Storage.Tables" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Aethers.Notebook.Storage.Tables" />
          </setting>
        </map>
        <map name="MapWeb:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWebInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/WebInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="UploadWorker" generation="1" functional="0" release="0" software="D:\pszdp1\Documents\Projects\Horizon\VisualStudio\Aether-s-Notebook-Azure-Server\Aether-s-Notebook-Azure-Server\csx\Release\roles\UploadWorker" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Aethers.Notebook.DataAccess.ConnectionString" defaultValue="" />
              <aCS name="Aethers.Notebook.Diagnostics.BufferQuota" defaultValue="" />
              <aCS name="Aethers.Notebook.Diagnostics.LogLevel" defaultValue="" />
              <aCS name="Aethers.Notebook.Diagnostics.TransferPeriod" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.ConnectionString" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.Container.UnknownData" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.Container.UploadedData" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.Containers" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.Queue.UploadedData" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.Queues" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.Tables" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;UploadWorker&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;UploadWorker&quot; /&gt;&lt;r name=&quot;Web&quot;&gt;&lt;e name=&quot;http&quot; /&gt;&lt;e name=&quot;https&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorkerInstances" />
            <sCSPolicyFaultDomainMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorkerFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="Web" generation="1" functional="0" release="0" software="D:\pszdp1\Documents\Projects\Horizon\VisualStudio\Aether-s-Notebook-Azure-Server\Aether-s-Notebook-Azure-Server\csx\Release\roles\Web" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="http" protocol="http" portRanges="80" />
              <inPort name="https" protocol="https" portRanges="443">
                <certificate>
                  <certificateMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/SSLCertificate" />
                </certificate>
              </inPort>
            </componentports>
            <settings>
              <aCS name="Aethers.Notebook.DataAccess.ConnectionString" defaultValue="" />
              <aCS name="Aethers.Notebook.Dataware.Description" defaultValue="" />
              <aCS name="Aethers.Notebook.Dataware.LogoUri" defaultValue="" />
              <aCS name="Aethers.Notebook.Dataware.Namespace" defaultValue="" />
              <aCS name="Aethers.Notebook.Dataware.ResourceName" defaultValue="" />
              <aCS name="Aethers.Notebook.Dataware.ResourceUri" defaultValue="" />
              <aCS name="Aethers.Notebook.Dataware.WebUri" defaultValue="" />
              <aCS name="Aethers.Notebook.Diagnostics.BufferQuota" defaultValue="" />
              <aCS name="Aethers.Notebook.Diagnostics.LogLevel" defaultValue="" />
              <aCS name="Aethers.Notebook.Diagnostics.TransferPeriod" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.ConnectionString" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.Container.UnknownData" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.Container.UploadedData" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.Containers" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.Queue.UploadedData" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.Queues" defaultValue="" />
              <aCS name="Aethers.Notebook.Storage.Tables" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;Web&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;UploadWorker&quot; /&gt;&lt;r name=&quot;Web&quot;&gt;&lt;e name=&quot;http&quot; /&gt;&lt;e name=&quot;https&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="Sandbox" defaultAmount="[100,100,100]" defaultSticky="false" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0SSLCertificate" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/SSLCertificate" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="SSLCertificate" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/WebInstances" />
            <sCSPolicyFaultDomainMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/WebFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="UploadWorkerFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="WebFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="UploadWorkerInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="WebInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="321b3b26-96e7-49d6-ae14-f00f6722396e" ref="Microsoft.RedDog.Contract\ServiceContract\Aether_s_Notebook_Azure_ServerContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="76d423a0-3f2f-44e6-8a82-f906a4fb2c54" ref="Microsoft.RedDog.Contract\Interface\Web:http@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web:http" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="449caeab-c0c1-41f9-b4bb-885a117c3596" ref="Microsoft.RedDog.Contract\Interface\Web:https@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web:https" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>