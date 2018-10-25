<template>
    <div class="field-group">
        <label :for="controlId" v-bind:class="{ animated: animatedLabel, hasContent: hasSelectedValue }" v-if="label">
            <span class="fixed-label">{{ label }}</span>
            <span class="animated-label">{{ label }}</span>
        </label>
        <input type="text" ref="input" class="fill-width" :name="controlId" :id="controlId" v-model="formValue" v-on:input="debounceInput($event)" :placeholder="label">
    </div>
</template>
<script>
    export default {
        name: 'form-input',
        model: {
            event: 'value-changed'
        },
        data() {
            return {
                formValue: this.value
            }
        },
        methods: {
            // Wait for the user to stop typing for specified milliseconds before processing the change
            debounceInput: _.debounce(function (event) {
                this.$emit('value-changed', this.$refs.input.value);
            }, 750),
            clear: function () {
                this.formValue = null;
            }
        },
        computed: {
            controlId: function () {
                return this.idPrefix + this.id;
            },
            hasSelectedValue: function () {
                return this.formValue && this.formValue != '0';
            }
        },
        watch: {
            value: function () {
                this.formValue = this.value;
            }
        },
        props: {
            value: {
                required: true
            },
            placeholder: {
                required: false
            },
            label: {
                required: false
            },
            animatedLabel: {
                type: Boolean,
                default: true
            },
            id: {
                default: "0"
            },
            idPrefix: {
                default: "self-reg_"
            }
        }
    }
</script>