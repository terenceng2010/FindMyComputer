import Config from "../config.js";
import Api from "../api.js";
import ComputerCard from './ComputerCard.js';
import ComputerFilterPanel from './ComputerFilterPanel.js';

export default {
    name: 'ComputerList',
    components: {
        ComputerCard,
        ComputerFilterPanel
    },
    data() {
        return {
            computers: [],
            stat: {}
        };
    },
    mounted() {
        this.config = new Config();
        this.baseUrl = this.config.baseUrl;
        this.api = new Api(this.baseUrl);
        this.api.getComputers().then(result => {
            this.computers = result;
        });
        this.api.getComputerStat().then(result => {
            this.stat = result;
        })

    },
    template: `
    <div>
      <p>
        <router-link class='uk-margin-right' to="/">Start Over</router-link> 
      </p>
      <main>
      <section>
        <div uk-grid>
            <ComputerFilterPanel :stat='stat'/>
            <div class='choices uk-flex uk-flex-column'>
                <h3>{{this.computers.length}} Result(s)</h3>
                <computer-card :query="query" :c="c" v-for="c in computers" :key="c.computerId" class="uk-width-1-1 uk-padding uk-margin-bottom">                
                </computer-card>             
            </div>
        </div>
      </section>    
      </main>
    </div>
  `,
};