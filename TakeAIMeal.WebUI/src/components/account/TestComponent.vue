<template>
    <h1>Zaloguj się</h1>
    <form id="demo">
        <!-- text -->
        <p>
            <input type="text" v-model="msg">
            {{msg}}
        </p>
        <!-- checkbox -->
        <p>
            <input type="checkbox" v-model="checked">
            {{checked ? "yes" : "no"}}
        </p>
        <!-- radio buttons -->
        <p>
            <input type="radio" name="picked" value="one" v-model="picked">
            <input type="radio" name="picked" value="two" v-model="picked">
            {{picked}}
        </p>
        <!-- select -->
        <p>
            <select v-model="selected">
            <option>one</option>
            <option>two</option>
            </select>
            {{selected}}
        </p>
        <!-- multiple select -->
        <p>
            <select v-model="multiSelect" multiple>
            <option>one</option>
            <option>two</option>
            <option>three</option>
            </select>
            {{multiSelect}}
        </p>
        <!-- <p><pre>data: {{$data | json 2}}</pre></p> -->
    </form>
</template>

<script>
import { defineComponent } from 'vue'
import { useI18n } from 'vue-i18n'
export default defineComponent({
    name: 'TestComponent',
    setup() {
        const { t } = useI18n({
            inheritLocale: true,
            useScope: 'local'
        })

        return { t }
    },
    data() {
        return {
            msg      : 'hi!',
            checked  : true,
            picked   : 'one',
            selected : 'two',
            multiSelect: ['one', 'three']
        }
    },
})
</script>