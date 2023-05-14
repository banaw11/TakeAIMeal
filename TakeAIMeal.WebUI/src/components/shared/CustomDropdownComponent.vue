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
                        @select="onChange(value, id)"
                        @remove="onChange(value, id)"
                        label="name"
                        :custom-label="translateLabel"
                        track-by="value" />
    </div>
</template>

<script>
    /* eslint-disable */
    import { useI18n } from 'vue-i18n'
    import VueMultiselect from 'vue-multiselect'
    export default {
        name: 'custom-dropdown',
        components: { VueMultiselect },
        props: ['data', 'modelValue', 'multi', 'translatePath'],
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
        },
        methods: {
            onChange(value, id) {
                this.$emit('change')              
            },
            translateLabel(item) {
                if (this.translatePath) {
                    return this.t(`QuickRecipe.Dictionary.${item.name}`);
                }
                return item.name;
            }
        }
    };
</script>
