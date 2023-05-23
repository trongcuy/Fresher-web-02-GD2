import mutation from "./mutation"
import action from "./action"
import getter from "./getter"
import state from "./state"
const Store = {
    state: state,
    getters: getter,
    mutations: mutation,
    actions: action
}
export default Store