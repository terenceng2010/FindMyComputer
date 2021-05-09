export default {
    name: `ComputerCard`,
    props: ['query', 'c'],
    template: `
    <div class="uk-card uk-card-default uk-card-body">
        <h3 class="uk-card-title">
            Machine {{c.computerId}}
        </h3>
        <div class="uk-column-1-2 uk-column-divider">
            <p><span class="uk-label">RAM</span>
                <span class="uk-margin-left" v-if='c.ram < 1000'>{{c.ram}}MB</span>
                <span class="uk-margin-left" v-else>{{c.ram/1000}}GB</span>
            </p>
            <p>
                <span class="uk-label uk-label-danger" v-if="query && query=='HarddiskSize'">HARDDISK</span>
                <span class="uk-label" v-else>HARDDISK</span>
                <span class="uk-margin-left" v-if='c.harddiskSize < 1000'>{{c.harddiskSize}}GB {{c.harddiskType}}</span>
                <span class="uk-margin-left" v-else>{{c.harddiskSize/1000}}TB {{c.harddiskType}}</span>
            </p>
            <p>
                <span class="uk-label uk-label-danger" v-if="query && query=='ConnectorCount'">CONNECTIVITY({{c.connectorCount}})</span>
                <span class="uk-label" v-else>CONNECTIVITY({{c.connectorCount}})</span> 
                <span class="uk-margin-left" v-for="conn in c.connectors">{{conn.name}} x {{conn.quantity}}</span>
            </p>
            <p><span class="uk-label">GPU</span> {{c.graphicsCardModel}}</p>
            <p>
                <span class="uk-label uk-label-danger" v-if="query && query=='TowerWeight'">WEIGHT</span>
                <span class="uk-label" v-else>WEIGHT</span>
                {{c.towerWeight}}KG ({{ Math.round(c.towerWeight*2.20462262*10)/10}}LB)
            </p>
            <p><span class="uk-label">POWER SUPPLY</span> {{c.powerSupplyWatt}}W</p>  
            <p><span class="uk-label">CPU</span> {{c.cpuModel}}</p>
        </div>        
    </div>
    `
};