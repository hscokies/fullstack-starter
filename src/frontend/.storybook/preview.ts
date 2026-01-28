import { type Preview, setup } from '@storybook/vue3-vite';
import { classNamePlugin } from '@/shared/plugins/class-name.ts';

setup(app => {
    app.use(classNamePlugin);
});

const preview: Preview = {
    parameters: {
        controls: {
            matchers: {
                color: /(background|color)$/i,
                date: /Date$/i,
            },
        },
        backgrounds: {
            options: {
                dark: {
                    name: 'Dark',
                    value: '#232634',
                },
            },
        },
    },
};

export default preview;
