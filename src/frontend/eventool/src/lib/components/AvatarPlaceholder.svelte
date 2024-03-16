<script lang="ts">
    const {avatarClasses = ""} = $props<{avatarClasses: string}>();
</script>

<div class="avatar placeholder text-neutral-content bg-primary {avatarClasses} flex flex-col items-center justify-center text-center">
    <div>
        <slot />
    </div>
</div>