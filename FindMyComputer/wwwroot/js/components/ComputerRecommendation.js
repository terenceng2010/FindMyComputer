export default {
    name: 'ComputerRecommendation',
    props: ['query'],
    components: {
    },
    data() {
        return {
            count: 0,
        };
    },
    template: `
    <div>
      <p>
        <router-link class='uk-margin-right' to="/">Start Over</router-link> 
        <router-link class='uk-margin-right' to="/about">Show me all the computers</router-link>
      </p>
      <main>
      <section>
        <h1>{{query}}</h1>
        <h2>How about some of these?</h2>
        <div class='choices uk-flex uk-flex-column'>
            <router-link :to="{ path: 'recommend' }" to="/about" class="uk-button uk-button-default uk-width-1-1 uk-padding uk-margin-bottom uk-text-large">Machine A</router-link> 
            <router-link :to="{ path: 'recommend' }" to="/about" class="uk-button uk-button-default uk-width-1-1 uk-padding uk-margin-bottom uk-text-large">Machine B</router-link> 
            <router-link :to="{ path: 'recommend' }" to="/about" class="uk-button uk-button-default uk-width-1-1 uk-padding uk-margin-bottom uk-text-large">Machine C</router-link> 
        </div>
      </section>    
      </main>
    </div>
  `,
};