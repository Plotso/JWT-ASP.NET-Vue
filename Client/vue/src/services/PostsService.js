import axios from 'axios'

const postsApi = axios.create({ //ToDo: Public posts   && 1 more for posts && 1 more for comments
    baseURL: process.env.VUE_APP_JWTSHOWCASE_POSTS_GATEWAY_ADDRESS,//process.env.JWTSHOWCASE_POSTS_GATEWAY_ADDRESS_DOCKER,
    withCredentials: false, // This is the default
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json'
    }
})

export default{
    // Posts
    getPublicPosts(){
        return postsApi.get('/PublicPosts/GetAll'); //ToDo: Public posts
    },
    getPosts(){ //ToDo: Add query logic
        const tkn = JSON.parse(localStorage.getItem('tk_ex')).token;
        return postsApi.get('/Posts/GetAll', {headers: { 'Authorization' : 'Bearer ' + tkn}});
    },
    create(postInputModel){
        const tkn = JSON.parse(localStorage.getItem('tk_ex')).token;
        return postsApi.post('/Posts/Create', postInputModel, {headers: { 'Authorization' : 'Bearer ' + tkn}});
    },
    update(id, postInputModel){        
        const tkn = JSON.parse(localStorage.getItem('tk_ex')).token;
        return postsApi.post('/Posts/Edit', {id, postInputModel}, {headers: { 'Authorization' : 'Bearer ' + tkn}});
    },
    delete(id){
        const tkn = JSON.parse(localStorage.getItem('tk_ex')).token;
        return postsApi.post('/Posts/Delete', id, {headers: { 'Authorization' : 'Bearer ' + tkn}});
    }
}