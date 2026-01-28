<script setup lang="ts">
import { useCustomFocus } from '@/shared/hooks';
import { ref } from 'vue';

const { focused, onFocus, onBlur } = useCustomFocus();
const props = defineProps({
    value: String,
    type: {
        type: String,
        default: 'text',
    },
    placeholder: {
        type: String,
        required: false,
    },
    disabled: {
        type: Boolean,
        default: false,
    },
    invalid: {
        type: Boolean,
        default: false,
    },
    readonly: {
        type: Boolean,
        default: false,
    },
});

const fieldRef = ref<HTMLInputElement | null>(null);

function onFocusIn(e: FocusEvent) {
    if (e.target === fieldRef.value) {
        onFocus();
    }
}
</script>

<template>
    <div :class="$cn({ focused, disabled, readonly, invalid })" @focusin="onFocusIn" @focusout="onBlur">
        <div v-if="$slots.prefix" :class="$cn('prefix')">
            <slot name="prefix" />
        </div>
        <input
            ref="fieldRef"
            v-bind="$attrs"
            :type="props.type"
            :class="$cn('field')"
            :disabled="props.disabled"
            :readonly="props.readonly"
            :placeholder="props.placeholder"
        />
        <div v-if="$slots.suffix" :class="$cn('suffix')">
            <slot name="suffix" />
        </div>
    </div>
</template>

<style lang="scss" scoped>
@use 'src/shared/ui/utils' as utils;
@use 'src/shared/ui/variables' as vars;
@use 'src/shared/ui/colors' as colors;

$height: 33px;
$border: 2px solid;
$border-radius: 4px;
$box-shadow: 0 0 0 2px;
$font-size: 16px;
$padding: 7px 10px;

.ui-input {
    display: inline-flex;
    align-items: stretch;
    height: var(--ui-input-height, $height);
    background: var(--ui-input-background, colors.$color-control-bg);
    border: $border var(--ui-input-border, colors.$color-control-border);
    color: var(--ui-input-text-color, colors.$color-text-primary);
    border-radius: var(--ui-input-border-radius, $border-radius);
    font-size: var(--ui-input-font-size, $font-size);

    @include utils.transitions(background-color, border-color, box-shadow);

    &__suffix,
    &__prefix {
        display: grid;
        place-items: center;
        height: 100%;
    }

    &__field {
        flex: 1;
        border: none;
        background: transparent;
        color: inherit;
        font-size: inherit;
        padding: var(--ui-input-field-padding, $padding);

        &:focus {
            outline: none;
        }

        &:read-only {
            border: none !important;
        }

        &::placeholder {
            color: colors.$color-text-muted;
        }

        &[type='number']::-webkit-outer-spin-button,
        &[type='number']::-webkit-inner-spin-button {
            margin: 0;
            appearance: none;
        }

        &[type='number'] {
            appearance: textfield;
        }
    }

    &:hover {
        background: var(--ui-input-hover-background, colors.$color-control-bg-hover);
    }

    &--invalid {
        border: $border var(--ui-input-invalid-border, colors.$color-error);
    }

    &--focused {
        background: var(--ui-input-focus-background, colors.$color-control-bg);
        border: $border var(--ui-input-focus-border, colors.$color-control-border-focus);
        box-shadow: var(--ui-input-focus-shadow, $box-shadow colors.$color-focus-ring);
    }

    &--invalid#{&}--focused {
        background: var(--ui-input-invalid-focus-background, colors.$color-control-bg);
        border: $border var(--ui-input-invalid-focus-border, colors.$color-error);
        box-shadow: var(--ui-input-invalid-focus-shadow, $box-shadow colors.$color-error);
    }

    &--readonly {
        pointer-events: none;
        background: var(--ui-input-readonly-background, colors.$color-control-bg-disabled);
        border: $border var(--ui-input-readonly-border, colors.$color-control-border);
        color: var(--ui-input-readonly-color, colors.$color-disabled-text);
    }

    &--disabled {
        pointer-events: none;
        background: var(--ui-input-disabled-background, colors.$color-control-bg-disabled);
        border: $border var(--ui-input-disabled-border, colors.$color-disabled-border);
        color: var(--ui-input-disabled-color, colors.$color-disabled-text);
    }
}
</style>
