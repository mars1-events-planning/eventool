<script lang="ts">
    const { name } = $props<{name: string}>()
</script>

<div class="drawer drawer-end w-auto">
    <input id={name} type="checkbox" class="drawer-toggle" />
    <div class="drawer-content">
        <slot name="content" />
    </div>
    <div class="drawer-side z-[2]">
        <label for={name} aria-label="close sidebar" class="drawer-overlay"></label>
        <slot name="side" />
    </div>
</div>