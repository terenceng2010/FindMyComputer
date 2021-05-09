export default {
    name: `ComputerCard`,
    template: `
    <div class="uk-card uk-card-body">
        <h3 class="uk-card-title">
            <slot name="title"></slot>
        </h3>
        <slot name="content"></slot>
    </div>
    `
};