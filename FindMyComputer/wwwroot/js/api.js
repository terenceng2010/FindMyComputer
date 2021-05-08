export default class Api {
    constructor(baseUrl) {
        // setup
        this.baseUrl = baseUrl;
    }
    async getBooks() {
        const response  = await fetch(this.baseUrl + 'api/books')
        return response.json();
    }

    async getBookTop10(bookId) {
        const response = await fetch(this.baseUrl + 'api/books/' + bookId)
        return response.json();
    }

    async getBookSearchWord(bookId, keyword) {
        const response = await fetch(this.baseUrl + 'api/books/' + bookId + '?query=' + keyword)
        return response.json();
    }
}