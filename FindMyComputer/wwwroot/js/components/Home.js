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
      {{count}}
      <BaseButton @click='count++'/>
    </div>
  `,
};