export default class Config {
    constructor() {

        //Usually, this is stored in webpack config files of different env (prod, uat, dev...)
        //, and being transformed during compilation
        this.baseUrl = 'http://localhost:37450/';
    }
}