<template>
    <div class="rol__wrapper">
        <div class="rol__window">
            <div class="rol__header">
                {{ release.Title }}
                <div class="rol__close icon-x-close" v-on:click="closeROL"></div>
            </div>
            <div class="rol__document" v-html="release.Text"></div>
            <div class="rol__progress"></div>
            <div class="rol__footer" v-show="!rolScrolled">
                <p>View/Scroll to the bottom of the document to sign</p>
            </div>
            <transition name="slideup-fade">
                <div class="rol__footer on-top" v-show="rolScrolled">
                    <div class="rol__signmessage">By checking below, I represent that I am <span style="text-decoration:underline">{{ person.Fullname }}</span>, a parent or legal guardian, or otherwise legally authorized to sign on their behalf.</div>
                    <div class="sign-form">
                        <div class="agree-checkbox">
                            <div class="checkbox checkbox-alt" v-bind:class="{invalid: !release.agreeChecked}">
                                <input type="checkbox" id="rol-agree" name="cb-alt" v-model="release.agreeChecked" v-on:change="changeAgreedBox" />
                                <div class="checkbox__label" for="cb-alt"></div>
                            </div>
                            <label>{{ person.ReleaseDescription }}</label>
                        </div>
                        <div class="field-group" v-if="release.ElectronicSignatureRequired">
                            <input type="text" ref="typename" class="medium" name="signFullName" id="SignFullName" v-model="release.TypedFullname" :placeholder="'TYPE: ' + person.Fullname" style="width:100%" v-on:keyup="updateCanSign" v-on:keyup.enter="signROL" autocomplete="off" />
                        </div>
                        <a class="btn btn-primary" v-on:click="signROL" v-bind:class="{disabled: !canSign}">Sign</a>
                    </div>
                </div>
            </transition>
        </div>
    </div>
</template>
<script>
    export default {
        data: function () {
            return {
                showWindow: true,
                agreeChecked: false,
                rolScrolled: false,
                canSign: false
            }
        },
        props: {
            release: Object,
            afterSign: Function,
            person: Object
        },
        validations() {
            return {
                release: {
                    agreeChecked: {
                        required
                    },
                    TypedFullname: {
                        required
                    }
                }
            }
        },
        updated: function () {
            var self = this

            var rolScroller = $('.rol__document').first()
            var rol_read_bar = $('.rol__progress')
            var rolWindowWidth = $('.rol__window').width()
            var rolReadHeight = rolScroller.prop('scrollHeight')
            var rolWindowHeight = rolScroller.outerHeight()
            var scrollAmt = (rolWindowHeight / (rolReadHeight - rolScroller.scrollTop()))
            scrollAmt = scrollAmt > 1 ? 1 : scrollAmt
            rol_read_bar.width(rolWindowWidth * scrollAmt)

            rolScroller.scroll(function () {
                scrollAmt = (rolWindowHeight / (rolReadHeight - rolScroller.scrollTop()))
                scrollAmt = scrollAmt > 1 ? 1 : scrollAmt
                rol_read_bar.width(rolWindowWidth * scrollAmt)
                self.rolScrolled = scrollAmt > 0.90
                self.updateCanSign()
            })
        },
        methods: {
            isReadyToSign: function () {
                var self = this

                return (self.rolScrolled && self.release.agreeChecked && (self.release.TypedFullname == self.person.Fullname))
            },
            changeAgreedBox: function () {
                var self = this

                if (self.release.agreeChecked && this.$refs.typename) {
                    this.$refs.typename.focus()
                }
                self.updateCanSign()
            },
            updateCanSign: function () {
                var self = this
                var isSigned = true

                if (this.release.ElectronicSignatureRequired) {
                    isSigned = self.release.TypedFullname.toLowerCase().trim() === self.person.Fullname.toLowerCase().trim()
                }
                self.canSign = self.rolScrolled && self.release.agreeChecked && isSigned
                var checkCanSign = self.rolScrolled && self.release.agreeChecked && isSigned
                self.$set(self, 'canSign', checkCanSign)
            },
            signROL: function () {
                var rolScroller = $('.rol__document').first()

                if (this.canSign) {
                    this.$set(this.release, 'Signed', true)
                    this.$emit('releaseSigned')
                    rolScroller.scrollTop(0)
                    this.afterSign(this.release)
                    $('body').removeClass('no-scroll')
                }
            },
            closeROL: function () {
                var rolScroller = $('.rol__document').first()

                var rel = this.release
                rel.Signed = false
                this.$set(this, 'release', rel)
                rolScroller.scrollTop(0)
                this.afterSign(this.release)
                $('body').removeClass('no-scroll')
            }
        }
    }
</script>