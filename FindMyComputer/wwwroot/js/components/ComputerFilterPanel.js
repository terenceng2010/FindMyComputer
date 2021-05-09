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
    props: ['stat'],
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
            this.$parent.api.getComputerStat().then(result => {
                this.stat = result;
            })
            this.form = Object.assign({}, formDefault);
        },
        search() {
            var formKeys = Object.keys(this.form);
            formKeys.forEach(key => {
                if (typeof this.form[key] === 'string' || this.form[key] instanceof String){
                    if (this.form[key] === '') { this.form[key] = null; }
                }
            })
            this.$parent.api.getComputersByFacetSearch(this.form).then(result => {
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
    template: `
    <div class="uk-card uk-card-body">
        Filter:
        <form>
        <p>Ram: {{stat.minRam}}~{{stat.maxRam}} <input v-model='form.minRam' type='number'/>~<input  v-model='form.maxRam' type='number'/>MB </p>
        <p>Harddisk Size: {{stat.minHarddiskSize}}~{{stat.maxHarddiskSize}} <input v-model='form.minHarddiskSize' type='number'/>~<input v-model='form.maxHarddiskSize' type='number'/>GB</p>
        <p>Harddisk Type:
            <label v-for='h in stat.harddiskTypes'>
                <input type='checkbox' :value='h' v-model="form.harddiskTypes"/> {{h}}
            </label>
        </p>        
        <p>Connectors:
            <label v-for='c in stat.connectorNames'>
                <input type='checkbox' :value='c' v-model="form.connectorNames"/> {{c}}
            </label>
        </p>
        <p>Graphics Card Model:
            <input v-model='form.graphicsCardModelKeyword'/>
        </p>
        <p>Weight: {{stat.minTowerWeight}}~{{stat.maxTowerWeight}} <input v-model='form.minTowerWeight' type='number'/>~<input v-model='form.maxTowerWeight' type='number'/>KG</p>
        <p>PSU Watt: {{stat.minPowerSupplyWatt}}~{{stat.maxPowerSupplyWatt}} <input v-model='form.minPowerSupplyWatt' type='number'/>~<input v-model='form.maxPowerSupplyWatt' type='number'/>W</p>
        <p>CPU Brand:
            <label v-for='c in stat.cpuBrandList'>
                <input type='checkbox' :value='c' v-model="form.cpuBrandList"/> {{c}}
            </label>
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