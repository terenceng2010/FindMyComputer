const formDefault = {
    minRam: null,
    maxRam: null,
    minHarddiskSize: null,
    maxHarddiskSize: null,
    harddiskTypes: [],
    graphicsCardModelKeyword: null,
    minTowerWeight: null,
    maxTowerWeight: null,
    minPowerSupplyWatt: null,
    maxPowerSupplyWatt: null,
    cpuModelKeyword: null,
    cpuBrandList: [],
    connectorNames: []
}
export default {
    name: `ComputerFilterPanel`,
    data() {
        return {
            form: {
                minRam: null,
                maxRam: null,
                minHarddiskSize: null,
                maxHarddiskSize: null,
                harddiskTypes: [],
                graphicsCardModelKeyword: null,
                minTowerWeight: null,
                maxTowerWeight: null,
                minPowerSupplyWatt: null,
                maxPowerSupplyWatt: null,
                cpuModelKeyword: null,
                cpuBrandList: [],
                connectorNames: []
            }
        };
    },
    methods:{
        resetSearch() {
            this.form = Object.assign({}, formDefault);
        },
        search() {
            var formKeys = Object.keys(this.form);
            formKeys.forEach(key => {
                if (typeof this.form[key] === 'string' || this.form[key] instanceof String){
                    if (this.form[key] === '') { this.form[key] = null; }
                }
            })
            this.$parent.api.getComputerByFacetSearch(this.form).then(result => {
                this.$parent.computers = result;
            })
        },
        formUpdated: function (newV, oldV) {
            console.log('the form object updated')
            this.search();
        }
    },
    created: function () {
        this.$watch('form', this.formUpdated, {
            deep: true
        })
    },
    mounted() {

    },
    template: `
    <div class="uk-card uk-card-body">
        Filter:
        <form>
        <p>Ram: <input v-model='form.minRam' type='number'/>~<input  v-model='form.maxRam' type='number'/>MB</p>
        <p>Harddisk Size: <input v-model='form.minHarddiskSize' type='number'/>~<input v-model='form.maxHarddiskSize' type='number'/>GB</p>
        <p>Harddisk Type:
            <label><input type='checkbox' value='SSD' v-model="form.harddiskTypes"/> SSD </label>
            <label><input type='checkbox' value='SDD' v-model="form.harddiskTypes"/> SDD </label>
            <label><input type='checkbox' value='HDD' v-model="form.harddiskTypes"/> HDD </label>
        </p>        
        <p>Connectors:
            <label><input type='checkbox' value='USB C' v-model="form.connectorNames"/> USB C        </label>
            <label><input type='checkbox' value='USB 3.0' v-model="form.connectorNames"/> USB 3.0    </label>
            <label><input type='checkbox' value='USB 2.0' v-model="form.connectorNames"/> USB 2.0    </label>
        </p>
        <p>Graphics Card Model:
            <input v-model='form.graphicsCardModelKeyword'/>
        </p>
        <p>Weight: <input v-model='form.minTowerWeight' type='number'/>~<input v-model='form.maxTowerWeight' type='number'/>KG</p>
        <p>PSU Watt: <input v-model='form.minPowerSupplyWatt' type='number'/>~<input v-model='form.maxPowerSupplyWatt' type='number'/>W</p>
        <p>CPU Brand:
            <input type='checkbox' value='INTEL' v-model="form.cpuBrandList"> Intel
            <input type='checkbox' value='AMD' v-model="form.cpuBrandList"> AMD
        </p>
        <p>CPU Model:
            <input v-model='form.cpuModelKeyword'/>
        </p>
        </form>
        <button @click='search' class="uk-button uk-button-primary">Search</button>
        <button @click='resetSearch' class="uk-button uk-button-default">Reset</button>
        
    </div>
    `
};