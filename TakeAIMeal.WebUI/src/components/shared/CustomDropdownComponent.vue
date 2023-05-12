<template lang="html">
    <div>
        <VueMultiselect class="custom-dropdown" v-model="value"
                        :options="data"
                        :multiple="multi"
                        :placeholder="t('Dropdown.PleaseSelect')"
                        :selectLabel="t('Dropdown.PressEnterForSelect')"
                        :deselectLabel="t('Dropdown.PressEnterForRemove')"
                        :selectedLabel="t('Dropdown.Selected')"
                        :searchable="true"
                        label="name"
                        track-by="value" />
    </div>
</template>

<script>
    import { useI18n } from 'vue-i18n'
    import VueMultiselect from 'vue-multiselect'
    export default {
        name: 'custom-multi-dropdown',
        components: { VueMultiselect },
        props: ['data', 'modelValue', 'multi'],
        emits: ['update:modelValue'],
        computed: {
            value: {
                get() {
                    return this.modelValue
                },
                set(value) {
                    this.$emit('update:modelValue', value)
                }
            }
        },
        setup() {
            const { t } = useI18n({
                inheritLocale: true,
                useScope: 'local'
            })

            return { t }
        }
    };
</script>
