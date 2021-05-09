import Config from "../config.js";
import Api from "../api.js";
import ComputerCard from './ComputerCard.js';

export default {
    name: 'ComputerList',
    components: {
        ComputerCard
    },
    data() {
        return {
            computers: [],
        };
    },
    mounted() {
        this.config = new Config();
        this.baseUrl = this.config.baseUrl;
        this.api = new Api(this.baseUrl);
        this.api.getComputers().then(result => {
            this.computers = result;
        });

    },
    template: `
    <div>
      <p>
        <router-link class='uk-margin-right' to="/">Start Over</router-link> 
      </p>
      <main>
      <section>
        <div  class='choices uk-flex uk-flex-column'>
            <div  class='choices uk-flex uk-flex-column'>
                <computer-card :query="query" :c="c" v-for="c in computers" :key="c.computerId" class="uk-width-1-1 uk-padding uk-margin-bottom">                
                </computer-card> 
            </div>
        </div>
      </section>    
      </main>
    </div>
  `,
};