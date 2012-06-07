<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="Aether_s_Notebook_Azure_Server" generation="1" functional="0" release="0" Id="5b6281d5-9681-4048-873c-2396c3c13b77" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="Aether_s_Notebook_Azure_ServerGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="Web:http" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/LB:Web:http" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="UploadWorker:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:DataContextConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:DataContextConnectionString" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:UnrecognisedDataContainer" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:UnrecognisedDataContainer" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:UploadedDataContainer" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:UploadedDataContainer" />
          </maps>
        </aCS>
        <aCS name="UploadWorker:UploadedDataQueue" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorker:UploadedDataQueue" />
          </maps>
        </aCS>
        <aCS name="UploadWorkerInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapUploadWorkerInstances" />
          </maps>
        </aCS>
        <aCS name="Web:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="Web:DataContextConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:DataContextConnectionString" />
          </maps>
        </aCS>
        <aCS name="Web:Dataware.Description" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Dataware.Description" />
          </maps>
        </aCS>
        <aCS name="Web:Dataware.LogoUri" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Dataware.LogoUri" />
          </maps>
        </aCS>
        <aCS name="Web:Dataware.Namespace" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Dataware.Namespace" />
          </maps>
        </aCS>
        <aCS name="Web:Dataware.ResourceName" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Dataware.ResourceName" />
          </maps>
        </aCS>
        <aCS name="Web:Dataware.ResourceUri" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Dataware.ResourceUri" />
          </maps>
        </aCS>
        <aCS name="Web:Dataware.WebUri" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Dataware.WebUri" />
          </maps>
        </aCS>
        <aCS name="Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="Web:UploadedDataContainer" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:UploadedDataContainer" />
          </maps>
        </aCS>
        <aCS name="Web:UploadedDataQueue" defaultValue="">
          <maps>
            <mapMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/MapWeb:UploadedDataQueue" />
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
      </channels>
      <maps>
        <map name="MapUploadWorker:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/DataConnectionString" />
          </setting>
        </map>
        <map name="MapUploadWorker:DataContextConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/DataContextConnectionString" />
          </setting>
        </map>
        <map name="MapUploadWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapUploadWorker:UnrecognisedDataContainer" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/UnrecognisedDataContainer" />
          </setting>
        </map>
        <map name="MapUploadWorker:UploadedDataContainer" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/UploadedDataContainer" />
          </setting>
        </map>
        <map name="MapUploadWorker:UploadedDataQueue" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorker/UploadedDataQueue" />
          </setting>
        </map>
        <map name="MapUploadWorkerInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/UploadWorkerInstances" />
          </setting>
        </map>
        <map name="MapWeb:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/DataConnectionString" />
          </setting>
        </map>
        <map name="MapWeb:DataContextConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/DataContextConnectionString" />
          </setting>
        </map>
        <map name="MapWeb:Dataware.Description" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Dataware.Description" />
          </setting>
        </map>
        <map name="MapWeb:Dataware.LogoUri" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Dataware.LogoUri" />
          </setting>
        </map>
        <map name="MapWeb:Dataware.Namespace" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Dataware.Namespace" />
          </setting>
        </map>
        <map name="MapWeb:Dataware.ResourceName" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Dataware.ResourceName" />
          </setting>
        </map>
        <map name="MapWeb:Dataware.ResourceUri" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Dataware.ResourceUri" />
          </setting>
        </map>
        <map name="MapWeb:Dataware.WebUri" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Dataware.WebUri" />
          </setting>
        </map>
        <map name="MapWeb:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWeb:UploadedDataContainer" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/UploadedDataContainer" />
          </setting>
        </map>
        <map name="MapWeb:UploadedDataQueue" kind="Identity">
          <setting>
            <aCSMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web/UploadedDataQueue" />
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
          <role name="UploadWorker" generation="1" functional="0" release="0" software="D:\pszdp1\Documents\Projects\Horizon\VisualStudio\Aether-s-Notebook-Azure-Server\Aether-s-Notebook-Azure-Server\csx\Debug\roles\UploadWorker" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="DataContextConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="UnrecognisedDataContainer" defaultValue="" />
              <aCS name="UploadedDataContainer" defaultValue="" />
              <aCS name="UploadedDataQueue" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;UploadWorker&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;UploadWorker&quot; /&gt;&lt;r name=&quot;Web&quot;&gt;&lt;e name=&quot;http&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
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
          <role name="Web" generation="1" functional="0" release="0" software="D:\pszdp1\Documents\Projects\Horizon\VisualStudio\Aether-s-Notebook-Azure-Server\Aether-s-Notebook-Azure-Server\csx\Debug\roles\Web" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="http" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="DataContextConnectionString" defaultValue="" />
              <aCS name="Dataware.Description" defaultValue="" />
              <aCS name="Dataware.LogoUri" defaultValue="" />
              <aCS name="Dataware.Namespace" defaultValue="" />
              <aCS name="Dataware.ResourceName" defaultValue="" />
              <aCS name="Dataware.ResourceUri" defaultValue="" />
              <aCS name="Dataware.WebUri" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="UploadedDataContainer" defaultValue="" />
              <aCS name="UploadedDataQueue" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;Web&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;UploadWorker&quot; /&gt;&lt;r name=&quot;Web&quot;&gt;&lt;e name=&quot;http&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
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
    <implementation Id="c3352c50-3391-4b11-b14c-ecf86448dac3" ref="Microsoft.RedDog.Contract\ServiceContract\Aether_s_Notebook_Azure_ServerContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="97aa1d27-4848-49a9-8bf5-6605896b60ed" ref="Microsoft.RedDog.Contract\Interface\Web:http@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/Aether_s_Notebook_Azure_Server/Aether_s_Notebook_Azure_ServerGroup/Web:http" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>