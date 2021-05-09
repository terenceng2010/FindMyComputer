export default class Api {
    constructor(baseUrl) {
        // setup
        this.baseUrl = baseUrl;
    }

    async getComputerStat() {
        const response = await fetch(this.baseUrl + 'api/Computers/stat')
        return response.json();
    }

    async getComputers() {
        const response = await fetch(this.baseUrl + 'api/Computers')
        return response.json();
    }

    async getComputersBySort(key, isDesc) {
        const response = await fetch(this.baseUrl + 'api/Computers/sort?limit=3&isDesc=' + isDesc + '&key=' + key)
        return response.json();
    }

    async getComputersByFacetSearch(facetSearchObj) {
        const response = await
            fetch(this.baseUrl + 'api/Computers/facetsearch', {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                method: "POST",
                body: JSON.stringify(facetSearchObj)
            });
        return response.json();
    }


}