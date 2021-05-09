import Config from "../config.js";
import Api from "../api.js";
import ComputerCard from './ComputerCard.js';

export default {
    name: 'ComputerRecommendation',
    props: ['query','isDesc','title'],
    components: {
        ComputerCard
    },
    data() {
        return {
            count: 0,
            computers:[],
        };
    },
    mounted() {
        this.config = new Config();
        this.baseUrl = this.config.baseUrl;
        this.api = new Api(this.baseUrl);
        this.api.getComputersBySort(this.query,this.isDesc).then(result => {
            this.computers = result;
        });
        
    },
    template: `
    <div>
      <p>
        <router-link class='uk-margin-right' to="/">Start Over</router-link> 
        <router-link class='uk-margin-right' to="/about">Show me all the computers</router-link>
      </p>
      <main>
      <section>
        <h1>{{title}}</h1>
        <h2>How about some of these?</h2>
        <div  class='choices uk-flex uk-flex-column'>
            <computer-card v-for="c in computers" :key="c.computerId"
                class="uk-width-1-1 uk-padding uk-margin-bottom">
                <template v-slot:title>
                   Machine {{c.computerId}}
                </template>          
                <template v-slot:content>
                    <div class="uk-column-1-2 uk-column-divider">
                        <p><span class="uk-label">RAM</span> {{c.ram}}MB</p>
                        <p>
                            <span class="uk-label uk-label-danger" v-if="query=='HarddiskSize'">HARDDISK</span>
                            <span class="uk-label" v-else>HARDDISK</span>
                            {{c.harddiskSize}}GB {{c.harddiskType}}
                        </p>
                        <p>
                            <span class="uk-label uk-label-danger" v-if="query=='ConnectorCount'">CONNECTIVITY</span>
                            <span class="uk-label" v-else>CONNECTIVITY</span> 
                            <span class="uk-margin-left" v-for="conn in c.connectors">{{conn.name}} x {{conn.quantity}}</span>
                        </p>
                        <p><span class="uk-label">GPU</span> {{c.graphicsCardModel}}</p>
                        <p>
                            <span class="uk-label uk-label-danger" v-if="query=='TowerWeight'">WEIGHT</span>
                            <span class="uk-label" v-else>WEIGHT</span>
                            {{c.towerWeight}}KG
                        </p>
                        <p><span class="uk-label">POWER SUPPLY</span> {{c.powerSupplyWatt}}W</p>  
                        <p><span class="uk-label">CPU</span> {{c.cpuModel}}</p>
                    </div>
                </template>   
                
            </computer-card> 

        </div>
      </section>    
      </main>
    </div>
  `,
};