// src/components/Home.js
import BaseButton from './BaseButton.js';

export default {
    name: 'Home',
    components: {
        BaseButton,
    },
    data() {
        return {
            count: 0,
        };
    },
    template: `
    <div>
      <p>
        <router-link to="/list">Show me all the computers</router-link>
      </p>
      <main>
      <section>
        <h1>Welcome to "Find My Next Computer"! </h1>
        <h2>Which of the following sentences best describe the computer you are looking for?</h2>

        <div class='choices uk-flex uk-flex-column'>
            <router-link to="/recommend?q=TowerWeight&isDesc=false&title=Light+Weight" class="uk-button uk-button-default uk-width-1-1 uk-padding uk-margin-bottom uk-text-large">🪁 I want a lightweight machine.</router-link> 
            <router-link to="/recommend?q=HarddiskSize&isDesc=true&title=Large+Storage" class="uk-button uk-button-default uk-width-1-1 uk-padding uk-margin-bottom uk-text-large">🗃 I am a data hoarder. </router-link> 
            <router-link to="/recommend?q=ConnectorCount&isDesc=true&title=Connectivity" class="uk-button uk-button-default uk-width-1-1 uk-padding uk-margin-bottom uk-text-large">🔌 I have 20+ USB devices in my home.</router-link> 
        </div>
      </section>    
      </main>
    </div>
  `,
};