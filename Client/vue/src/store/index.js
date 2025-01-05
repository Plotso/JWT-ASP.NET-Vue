import { createStore } from 'vuex'
import * as post from '@/store/modules/post.js'
import * as authentication from '@/store/modules/authentication.js'
import * as alert from '@/store/modules/alert.js'

export default createStore({
  state: {
  },
  getters: {
  },
  mutations: {
  },
  actions: {
  },
  modules: { post, authentication, alert }
})
