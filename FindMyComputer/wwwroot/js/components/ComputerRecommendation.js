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
        <router-link class='uk-margin-right' to="/list">Show me all the computers</router-link>
      </p>
      <main>
      <section>
        <h1>{{title}}</h1>
        <h2>How about some of these?</h2>
        <div  class='choices uk-flex uk-flex-column'>
            <computer-card :query="query" :c="c" v-for="c in computers" :key="c.computerId" class="uk-width-1-1 uk-padding uk-margin-bottom">                
            </computer-card> 

        </div>
      </section>    
      </main>
    </div>
  `,
};