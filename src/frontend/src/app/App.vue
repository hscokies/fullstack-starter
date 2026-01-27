<script setup lang="ts"></script>

<template>
    <div :class="$cn()">
        <div :class="$cn('first-el')">
            <div :class="$cn('second-el', { active: true, number: 5, additional: 'str' })">
                {{ $t('Title.SignIn') }}
            </div>
        </div>
    </div>
</template>
